using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Persistense.MongoDb.Tools;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IRequestTargetQueryService
    {
        Task<List<RequestTargetQuery>> Get();
        Task<RequestTargetQuery> Get(long id);
        Task Create(RequestTargetParameter parameter);
        Task Update(RequestTargetParameter parameter);
        Task Update(RequestTargetQuery query);
        Task Remove(long id);
        Task<List<RequestTargetQuery>> GetByPredicate(PagingQueryModelContract pagingQuery);
        Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate);
        Task<long> GetCount(List<SearchParameter> parameters);
        Task<List<RequestTargetQuery>> GetAllRequestTargetByText(string query);
        Task<List<RequestTargetQuery>> GetAllRequestTargetByBasicInfo(UserSearchByBasicInfoQuery input);
        Task<List<RequestTargetQuery>> GetAllRequestTargetBySubjects(UserDashboardSearchBySubjectQuery input);
        Task<List<RequestTargetQuery>> GetAllRequestTargetByCharacteristic(UserDashboardSearchByCharactresticQuery input);
        Task<List<RequestTargetQuery>> GetRequestTargetsByIds(List<long> ids);

    }
    public class RequestTargetQueryService : IRequestTargetQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IBasicInfoQueryService _basicInfoQueryService;


        public RequestTargetQueryService(IMongoRepository repository, IBasicInfoQueryService basicInfoQueryService)
        {
            _repository = repository;
            _basicInfoQueryService = basicInfoQueryService;
        }

        public Task<List<RequestTargetQuery>> Get()
        {
            return _repository.RequestTargetQueryCollection.Find(requestTarget => true).ToListAsync();
        }

        public Task<RequestTargetQuery> Get(long id)
        {
            return _repository.RequestTargetQueryCollection.Find(requestTarget => requestTarget.Id == id).FirstOrDefaultAsync();
        }







        public async Task Create(RequestTargetParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }


                var labels = await _basicInfoQueryService.GetLabelByIds(parameter.LabelIds);
                var subjects = await _basicInfoQueryService.GetSubjectByIds(parameter.SubjectIds);
                //var requestOrgs = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgIds);

                await _repository.RequestTargetQueryCollection.InsertOneAsync(new RequestTargetQuery(parameter.Id,
                       parameter.Title, parameter.Icon, labels, subjects,
                       parameter.PublishEventDate, parameter.EventId, parameter.IsDocument));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(RequestTargetParameter parameter)
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
            query.Icon = parameter.Icon;
            query.IsDocument = parameter.IsDocument;


            query.LabelList = await _basicInfoQueryService.GetLabelByIds(parameter.LabelIds);
            query.SubjectList = await _basicInfoQueryService.GetSubjectByIds(parameter.SubjectIds);
            //query.RequestOrgList = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgIds);

            query.LastEventPublishDate = parameter.PublishEventDate;
            query.LastEventId = parameter.EventId;

            await _repository.RequestTargetQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

            await Request_ReplaceNewRequestTarget(query);
        }

        public async Task Update(RequestTargetQuery query)
        {
            await _repository.RequestTargetQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

            await Request_ReplaceNewRequestTarget(query);
        }

        public async Task Request_ReplaceNewRequestTarget(RequestTargetQuery requestTargetQuery)
        {
            var requests = (await _repository.RequestQueryCollection.FindAsync(m => m.RequestTarget.Id == requestTargetQuery.Id)).ToList();

            foreach (var item in requests)
            {
                item.RequestTarget = requestTargetQuery;
                item.Title = item.RequestAct.Title + " " + item.RequestTarget.Title;
                await _repository.RequestQueryCollection.ReplaceOneAsync(m => m.Id == item.Id, item);
            }
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.RequestTargetQueryCollection.DeleteOneAsync(requestTarget => requestTarget.Id == id);
            }
        }

        public Task<List<RequestTargetQuery>> GetByPredicate(PagingQueryModelContract pagingQuery)
        {
            var filters = Builders<RequestTargetQuery>.Filter.GenerateFilter(pagingQuery.SearchParameters);
            return _repository.RequestTargetQueryCollection
                .Find(filters)
                .Skip((pagingQuery.PageNumber - 1) * pagingQuery.PageSize)
                .Limit(pagingQuery.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate)
        {
            return _repository.RequestTargetQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(List<SearchParameter> parameters)
        {
            var filters = Builders<RequestTargetQuery>.Filter.GenerateFilter(parameters);
            return _repository.RequestTargetQueryCollection
                .Find(filters).CountDocumentsAsync();
        }

        public async Task<List<RequestTargetQuery>> GetAllRequestTargetByText(string query)
        {
            var result = new List<RequestTargetQuery>();
            var builder = Builders<RequestTargetQuery>.Filter;
            var filter = builder.Regex("Title", BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
            return await _repository.RequestTargetQueryCollection.Find(filter)?.ToListAsync();
        }

        public async Task<List<RequestTargetQuery>> GetAllRequestTargetByBasicInfo(UserSearchByBasicInfoQuery input)
        {
            var result = new List<RequestTargetQuery>();
            var builder = Builders<RequestTargetQuery>.Filter;
            if (input.LabelIds.Any())
            {
                var list = builder.ElemMatch(el => el.LabelList, el => input.LabelIds.Contains(el.Id));
                result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            }
            //TODO : Check For New Business
            //if (input.RequestOrgIds.Any())
            //{
            //    var list = builder.ElemMatch(el => el.RequestOrgList, el => input.RequestOrgIds.Contains(el.Id));
            //    result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            //}
            if (input.SubjectIds.Any())
            {
                var list = builder.ElemMatch(el => el.SubjectList, el => input.SubjectIds.Contains(el.Id));
                result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            }
            return result;
        }

        public async Task<List<RequestTargetQuery>> GetAllRequestTargetBySubjects(UserDashboardSearchBySubjectQuery input)
        {
            var result = new List<RequestTargetQuery>();
            var builder = Builders<RequestTargetQuery>.Filter;
            if (input.SubjectIds.Any())
            {
                var list = builder.ElemMatch(el => el.SubjectList, el => input.SubjectIds.Contains(el.Id));
                result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            }
            return result;
        }

        public async Task<List<RequestTargetQuery>> GetAllRequestTargetByCharacteristic(UserDashboardSearchByCharactresticQuery input)
        {
            var result = new List<RequestTargetQuery>();
            var builder = Builders<RequestTargetQuery>.Filter;
            if (input.LabelIds.Any())
            {
                var list = builder.ElemMatch(el => el.LabelList, el => input.LabelIds.Contains(el.Id));
                result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            }
            return result;
        }

        public async Task<List<RequestTargetQuery>> GetRequestTargetsByIds(List<long> ids)
        {
            var result = new List<RequestTargetQuery>();
            var builder = Builders<RequestTargetQuery>.Filter;
            if (ids.Any())
            {
                var list = builder.In("Id", ids);
                result.AddRange(await _repository.RequestTargetQueryCollection.Find(list).ToListAsync());
            }
            return result;
        }
    }

}