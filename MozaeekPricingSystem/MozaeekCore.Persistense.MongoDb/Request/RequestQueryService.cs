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
    public interface IRequestQueryService
    {
        Task<List<RequestQuery>> Get();
        Task<RequestQuery> Get(long id);
        Task Create(RequestParameter parameter);
        Task Update(RequestParameter parameter);
        Task Remove(long id);
        Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<RequestQuery, bool>> predicate);
        Task UpdateWithPoint(PointQuery point);
    }
    public class RequestQueryService : IRequestQueryService
    {
        private readonly IMongoRepository _repository;

        public RequestQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<RequestQuery>> Get()
        {
            return _repository.RequestQueryCollection.Find(request => true).ToListAsync();
        }

        public Task<RequestQuery> Get(long id)
        {
            return _repository.RequestQueryCollection.Find(request => request.Id == id).FirstOrDefaultAsync();
        }


        public async Task Create(RequestParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                var target = (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.TargetId)).FirstOrDefault();
                var act = (await _repository.RequestActQueryCollection.FindAsync(m => m.Id == parameter.ActId))
                    .FirstOrDefault();

                var pointList = await GetPointAndTheirChildrenByIds(parameter.PointIds);
                var title = act.Title + " " + target.Title;

                await _repository.RequestQueryCollection.InsertOneAsync(new RequestQuery(parameter.Id,
                  title, act, target, parameter.Documents, parameter.Nessesities, parameter.Actions, parameter.QualificationIds, pointList, parameter.PublishEventDate, parameter.EventId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(RequestParameter parameter)
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
            var obj = eventualConsistensy;
            var target = (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.Id)).FirstOrDefault();
            var act = (await _repository.RequestActQueryCollection.FindAsync(m => m.Id == parameter.ActId))
                .FirstOrDefault();
            var pointList = await GetPointAndTheirChildrenByIds(parameter.PointIds);
            var title = act.Title + " " + target.Title;
            obj.Title = title;
            obj.RequestTarget = target;
            obj.RequestAct = act;
            obj.Actions = parameter.Actions;
            obj.Documents = parameter.Documents;
            obj.Qualifications = parameter.QualificationIds;
            obj.Nessesities = parameter.Nessesities;
            obj.Points = pointList;
            obj.LastEventPublishDate = parameter.PublishEventDate;
            obj.LastEventId = parameter.EventId;
            await _repository.RequestQueryCollection.ReplaceOneAsync(m => m.Id == obj.Id, obj);
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.RequestQueryCollection.DeleteOneAsync(request => request.Id == id);
            }
        }

        public Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.RequestQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate)
        {
            return _repository.RequestQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<RequestQuery, bool>> predicate)
        {
            return _repository.RequestQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public async Task UpdateWithPoint(PointQuery point)
        {
            var filterbuildRequestTarget = Builders<RequestQuery>.Filter;
            var filterRqAct =
                filterbuildRequestTarget.ElemMatch(doc => doc.Points, el => el.Id == point.Id);

            var requestQueries = await _repository.RequestQueryCollection.Find(filterRqAct).ToListAsync();

            foreach (var item in requestQueries)
            {
                item.Points = item.Points.Select(m =>
                {
                    if (m.Id == point.Id)
                    {
                        m.Title = point.Title;
                    }
                    return m;
                }).ToList();

                await _repository.RequestQueryCollection.ReplaceOneAsync(m => m.Id == item.Id, item);
            }
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
    }

}