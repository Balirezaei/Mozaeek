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
    public interface IRequestTargetQueryService
    {
        Task<List<RequestTargetQuery>> Get();
        Task<RequestTargetQuery> Get(long id);
        Task Create(RequestTargetParameter parameter);
        Task Update(RequestTargetParameter parameter);
        Task Update(RequestTargetQuery query);
        Task Remove(long id);
        Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<RequestTargetQuery, bool>> predicate);
    }
    public class RequestTargetQueryService : IRequestTargetQueryService
    {
        private readonly IMongoRepository _repository;

        public RequestTargetQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<RequestTargetQuery>> Get()
        {
            return _repository.RequestTargetQueryCollection.Find(requestTarget => true).ToListAsync();
        }

        public Task<RequestTargetQuery> Get(long id)
        {
            return _repository.RequestTargetQueryCollection.Find(requestTarget => requestTarget.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<SubjectQuery>> GetSubjectAndTheirChildrenByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<SubjectQuery>();
            }
            var filterList = new List<FilterDefinition<SubjectQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<SubjectQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id || z.ParentId == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<SubjectQuery>.Filter.Or(filterList);
            return (await _repository.SubjectQueryCollection.FindAsync(orFilter)).ToList();
        }

        public async Task<List<LabelQuery>> GetLabelsAndTheirChildrenByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<LabelQuery>();
            }
            var filterList = new List<FilterDefinition<LabelQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<LabelQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id || z.ParentId == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<LabelQuery>.Filter.Or(filterList);

            return (await _repository.LabelQueryCollection.FindAsync(orFilter)).ToList();
        }

        public async Task<List<RequestOrgQuery>> GetRequestOrgAndTheirChildrenByIds(List<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new List<RequestOrgQuery>();
            }
            var filterList = new List<FilterDefinition<RequestOrgQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<RequestOrgQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id || z.ParentId == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<RequestOrgQuery>.Filter.Or(filterList);
            return (await _repository.RequestOrgQueryCollection.FindAsync(orFilter)).ToList();
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

                var labels = await GetLabelsAndTheirChildrenByIds(parameter.LabelIds);

                var subjects = await GetSubjectAndTheirChildrenByIds(parameter.SubjectIds);
                var requestOrgIds = await GetRequestOrgAndTheirChildrenByIds(parameter.RequestOrgIds);


                await _repository.RequestTargetQueryCollection.InsertOneAsync(new RequestTargetQuery(parameter.Id,
                       parameter.Title, labels, subjects, requestOrgIds,
                       parameter.PublishEventDate, parameter.EventId));

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

            query.SubjectList = await GetSubjectAndTheirChildrenByIds(parameter.SubjectIds);
            query.LabelList = await GetLabelsAndTheirChildrenByIds(parameter.LabelIds);
            query.RequestOrgList = await GetRequestOrgAndTheirChildrenByIds(parameter.RequestOrgIds);
            query.LastEventPublishDate = parameter.PublishEventDate;
            query.LastEventId = parameter.EventId;

            await _repository.RequestTargetQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

            await Request_ReplaceNewRequestTarget(query);
            await Announcement_ReplaceNewRequestTarget(query);
        }

        public async Task Update(RequestTargetQuery query)
        {
            await _repository.RequestTargetQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

            await Request_ReplaceNewRequestTarget(query);
            await Announcement_ReplaceNewRequestTarget(query);

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
        public async Task Announcement_ReplaceNewRequestTarget(RequestTargetQuery requestTargetQuery)
        {
            var requests = (await _repository.AnnouncementQueryCollection
                .FindAsync(m => m.RequestTarget.Id == requestTargetQuery.Id))
                .ToList();

            foreach (var item in requests)
            {
                item.RequestTarget = requestTargetQuery;
                await _repository.AnnouncementQueryCollection.ReplaceOneAsync(m => m.Id == item.Id, item);
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

        public Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.RequestTargetQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestTargetQuery>> GetByPredicate(Expression<Func<RequestTargetQuery, bool>> predicate)
        {
            return _repository.RequestTargetQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<RequestTargetQuery, bool>> predicate)
        {
            return _repository.RequestTargetQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }
    }

}