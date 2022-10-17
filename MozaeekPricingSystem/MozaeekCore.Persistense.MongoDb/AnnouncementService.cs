using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
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
        Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<AnnouncementQuery, bool>> predicate);
    }
    public class AnnouncementQueryService : IAnnouncementQueryService
    {
        private readonly IMongoRepository _repository;

        public AnnouncementQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AnnouncementQuery>> Get()
        {
            return _repository.AnnouncementQueryCollection.Find(announcement => true).ToListAsync();
        }

        public Task<AnnouncementQuery> Get(long id)
        {
            return _repository.AnnouncementQueryCollection.Find(announcement => announcement.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<PointQuery>> GetPointAndTheirChildrenByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<PointQuery>();
            }
            var filterList = new List<FilterDefinition<PointQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<PointQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id || z.ParentId == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<PointQuery>.Filter.Or(filterList);
            return (await _repository.PointQueryCollection.FindAsync(orFilter)).ToList();
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

                var points = await GetPointAndTheirChildrenByIds(parameter.PointIds);
                var requestTarget =
                   (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.RequestTargetId)).FirstOrDefault();

                await _repository.AnnouncementQueryCollection.InsertOneAsync(new AnnouncementQuery(parameter.Id,
                       parameter.Title, points, requestTarget,
                       parameter.PublishEventDate, parameter.EventId));

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

            query.PointList = await GetPointAndTheirChildrenByIds(parameter.PointIds);
            var requestTarget =
                (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.RequestTargetId)).FirstOrDefault();

            query.RequestTarget = requestTarget;
            query.LastEventPublishDate = parameter.PublishEventDate;
            query.LastEventId = parameter.EventId;
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

        public Task<List<AnnouncementQuery>> GetByPredicate(Expression<Func<AnnouncementQuery, bool>> predicate, PagingContract pagingContract)
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
    }

}