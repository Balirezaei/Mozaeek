using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IAnnouncementQueryService
    {
        Task<List<AnnouncementQuery>> Get();
        Task<AnnouncementQuery> Get(long id);
        Task Create(AnnouncementParameter parameter);
        Task Update(AnnouncementParameter parameter);
        Task UpdateWithPoint(PointQuery point);
        Task Remove(long id);

        Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate,
            PagingContract pagingContract);

        Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<AnnouncementQuery, bool>> predicate);
        Task<List<AnnouncementQuery>> GetAllAnnouncementForUserDashboard(AnnouncementUserDashboardPagingContract input);
        Task<List<AnnouncementQuery>> GetAllAnnouncementByTextSearch(string query);
        Task<List<AnnouncementQuery>> GetAllAnnouncementByRequestTarget(List<long> ids);
        Task<List<AnnouncementQuery>> GetAllAnnouncementByLabels(List<long> ids);
        Task<List<AnnouncementQuery>> GetAllAnnouncementByPoints(List<long> ids);
        Task AssignRequest(long announcementId, long requestId);
    }

    public class AnnouncementQueryService : IAnnouncementQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IBasicInfoQueryService _basicInfoQueryService;

        public AnnouncementQueryService(IMongoRepository repository, IBasicInfoQueryService basicInfoQueryService)
        {
            _repository = repository;
            _basicInfoQueryService = basicInfoQueryService;
        }

        public Task<List<AnnouncementQuery>> Get()
        {
            return _repository.AnnouncementQueryCollection.Find(announcement => true).ToListAsync();
        }

        public Task<AnnouncementQuery> Get(long id)
        {
            return _repository.AnnouncementQueryCollection.Find(announcement => announcement.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Create(AnnouncementParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                var points = await _basicInfoQueryService.GetPointByIds(parameter.PointIds);
                var labels = await _basicInfoQueryService.GetLabelByIds(parameter.LabelIds);
                var subjects = await _basicInfoQueryService.GetSubjectByIds(parameter.SubjectIds);
                var requestOrgs = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgIds);

                //, 
                //, , string imageUrl, string summary, DateTime releaseDate,
                //bool hasRequest, Guid lastEventId, DateTime lastEventPublishDate

                await _repository.AnnouncementQueryCollection.InsertOneAsync(new AnnouncementQuery(parameter.Id,
                    parameter.Title, parameter.Description, points, labels, subjects, requestOrgs,
                    parameter.ImageUrl, parameter.Summary, parameter.CreateDate, parameter.HasRequest,
                    parameter.EventId, parameter.PublishEventDate));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(AnnouncementParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.Id);
            if (eventualConsistensy == null)
            {
                return;
            }

            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }

            var query = eventualConsistensy;
            query.Title = parameter.Title;
            query.Description = parameter.Description;
            query.Summary = parameter.Summary;
            query.HasRequest = parameter.HasRequest;
            query.LabelList = await _basicInfoQueryService.GetLabelByIds(parameter.LabelIds);
            query.SubjectList = await _basicInfoQueryService.GetSubjectByIds(parameter.SubjectIds);
            query.RequestOrgList = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgIds);
            query.PointList = await _basicInfoQueryService.GetPointByIds(parameter.PointIds);

            query.LastEventPublishDate = parameter.PublishEventDate;
            query.LastEventId = parameter.EventId;
            query.ImageUrl = parameter.ImageUrl;

            await _repository.AnnouncementQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task UpdateWithPoint(PointQuery point)
        {
            var filterbuildRequestTarget = Builders<AnnouncementQuery>.Filter;
            var filterRqAct =
                filterbuildRequestTarget.ElemMatch(doc => doc.PointList, el => el.Id == point.Id);

            var requestQueries = await _repository.AnnouncementQueryCollection.Find(filterRqAct).ToListAsync();

            foreach (var item in requestQueries)
            {
                item.PointList = item.PointList.Select(m =>
                {
                    if (m.Id == point.Id)
                    {
                        m.Title = point.Title;
                    }

                    return m;
                }).ToList();
                await _repository.AnnouncementQueryCollection.ReplaceOneAsync(m => m.Id == item.Id, item);
            }
        }


        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.AnnouncementQueryCollection.DeleteOneAsync(announcement => announcement.Id == id);
            }
        }

        public Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate,
            PagingContract pagingContract)
        {
            return _repository.AnnouncementQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate)
        {
            return _repository.AnnouncementQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<AnnouncementQuery, bool>> predicate)
        {
            return _repository.AnnouncementQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public async Task<List<AnnouncementQuery>> GetAllAnnouncementForUserDashboard(
            AnnouncementUserDashboardPagingContract input)
        {
            var result = new List<AnnouncementQuery>();
            var builder = Builders<AnnouncementQuery>.Filter;
            //TODO: CHECK IMP
            //if (input.SubjectIds.Any())
            //{
            //    var filterSubjectList = builder.ElemMatch(el => el.RequestTarget.SubjectList, el => input.SubjectIds.Contains(el.Id));
            //    //var filterChildrenSubjectList = builder.ElemMatch(el => el.RequestTarget.SubjectChildren, el => input.SubjectIds.Contains(el.Id));
            //    result.AddRange(await _repository.AnnouncementQueryCollection.Find(filterSubjectList).ToListAsync());
            //    //result.AddRange(await _repository.AnnouncementQueryCollection.Find(filterChildrenSubjectList).ToListAsync());
            //}
            result = result.GroupBy(r => r.Id).Select(r => r.First()).ToList();
            return result;
        }

        public async Task<List<AnnouncementQuery>> GetAllAnnouncementByTextSearch(string query)
        {
            var result = new List<AnnouncementQuery>();
            var builder = Builders<AnnouncementQuery>.Filter;
            var filter = builder.Regex("Title",
                BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
            return await _repository.AnnouncementQueryCollection.Find(filter)?.ToListAsync();
        }

        public async Task<List<AnnouncementQuery>> GetAllAnnouncementByRequestTarget(List<long> ids)
        {
            var result = new List<AnnouncementQuery>();
            var builder = Builders<AnnouncementQuery>.Filter;
            if (ids.Any())
            {
                var list = builder.In("RequestTarget._id", ids);
                result.AddRange(await _repository.AnnouncementQueryCollection.Find(list).ToListAsync());
            }

            return result;
        }

        public async Task<List<AnnouncementQuery>> GetAllAnnouncementByLabels(List<long> ids)
        {
            var result = new List<AnnouncementQuery>();
            var builder = Builders<AnnouncementQuery>.Filter;
            if (ids.Any())
            {
                var list = builder.ElemMatch(el => el.LabelList, el => ids.Contains(el.Id));
                result.AddRange(await _repository.AnnouncementQueryCollection.Find(list).ToListAsync());
            }

            return result;
        }

        public async Task<List<AnnouncementQuery>> GetAllAnnouncementByPoints(List<long> ids)
        {
            var result = new List<AnnouncementQuery>();
            var builder = Builders<AnnouncementQuery>.Filter;
            if (ids.Any())
            {
                var list = builder.ElemMatch(el => el.PointList, el => ids.Contains(el.Id));
                result.AddRange(await _repository.AnnouncementQueryCollection.Find(list).ToListAsync());
            }

            return result;
        }

        public async Task AssignRequest(long announcementId, long requestId)
        {
            var announcement = await Get(announcementId);
            var request = await _repository.RequestQueryCollection
                .Find(announcement => announcement.Id == requestId)
                .FirstOrDefaultAsync();

            announcement.RequestQuery = request;

            await _repository.AnnouncementQueryCollection.ReplaceOneAsync(m => m.Id == announcementId, announcement);

        }

    }
}