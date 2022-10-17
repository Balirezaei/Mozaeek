using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IRequestOrgQueryService
    {
        Task<List<RequestOrgQuery>> Get();
        Task<RequestOrgQuery> Get(long id);
        Task Create(RequestOrgQuery requestOrg);
        Task Update(RequestOrgQuery requestOrgIn);
        Task Remove(long id);
        Task<List<RequestOrgQuery>> GetByPredicate(Expression<Func<RequestOrgQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<RequestOrgQuery>> GetByPredicate(Expression<Func<RequestOrgQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<RequestOrgQuery, bool>> predicate);
        Task<List<RequestOrgQuery>> GetAllLevelChildren(List<long> ids);
        Task<List<RequestOrgQuery>> GetAllByText(string query);
    }
    public class RequestOrgQueryService : IRequestOrgQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public RequestOrgQueryService(IMongoRepository repository, IRequestTargetQueryService requestTargetQueryService)
        {
            _repository = repository;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public Task<List<RequestOrgQuery>> Get()
        {
            return _repository.RequestOrgQueryCollection.Find(requestOrg => true).ToListAsync();
        }

        public Task<RequestOrgQuery> Get(long id)
        {
            return _repository.RequestOrgQueryCollection.Find(requestOrg => requestOrg.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(RequestOrgQuery requestOrg)
        {
            try
            {
                var eventualConsistensy = await Get(requestOrg.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                if (requestOrg.ParentId != null)
                {
                    var directParent = await Get(requestOrg.ParentId.Value);
                    directParent.HasChild = true;
                    await _repository.RequestOrgQueryCollection.ReplaceOneAsync(m => m.Id == directParent.Id, directParent);
                }
                await _repository.RequestOrgQueryCollection.InsertOneAsync(requestOrg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(RequestOrgQuery requestOrgIn)
        {
            var eventualConsistensy = await Get(requestOrgIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }

            if (eventualConsistensy.LastEventPublishDate > requestOrgIn.LastEventPublishDate)
            {
                return;
            }
            var query = eventualConsistensy;
            query.Title = requestOrgIn.Title;
            query.Icon = requestOrgIn.Icon;
            query.LastEventPublishDate = requestOrgIn.LastEventPublishDate;
            query.LastEventId = requestOrgIn.LastEventId;


            await _repository.RequestOrgQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

        }


        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                if (saved.ParentId != null)
                {
                    var parent = await Get(saved.ParentId.Value);
                    var otherChildren = await GetByPredicate(m => m.ParentId == saved.ParentId && m.Id != id);

                    parent.HasChild = otherChildren.Any();
                    await _repository.RequestOrgQueryCollection.ReplaceOneAsync(requestOrg => requestOrg.Id == parent.Id, parent);
                }
                await _repository.RequestOrgQueryCollection.DeleteOneAsync(requestOrg => requestOrg.Id == id);
            }
        }

        public Task<List<RequestOrgQuery>> GetByPredicate(Expression<Func<RequestOrgQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.RequestOrgQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestOrgQuery>> GetByPredicate(Expression<Func<RequestOrgQuery, bool>> predicate)
        {
            return _repository.RequestOrgQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<RequestOrgQuery, bool>> predicate)
        {
            return _repository.RequestOrgQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public async Task<List<RequestOrgQuery>> GetAllLevelChildren(List<long> ids)
        {
            var result = new List<RequestOrgQuery>();
            var builder = Builders<RequestOrgQuery>.Filter;
            var filter = builder.In("ParentId", ids);
            var children = _repository.RequestOrgQueryCollection.Find(filter)?.ToList();
            if (children.Any())
            {
                result.AddRange(children);
                foreach (var child in children)
                {
                    result.AddRange(await GetAllLevelChildren(new List<long>() { child.Id }));
                }
            }
            result = result.GroupBy(l => l.Id).Select(l => l.First()).ToList();
            return result;
        }

        public async Task<List<RequestOrgQuery>> GetAllByText(string query)
        {
            var result = new List<RequestOrgQuery>();
            var builder = Builders<RequestOrgQuery>.Filter;
            var filter = builder.Regex("Title", BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
            return _repository.RequestOrgQueryCollection.Find(filter)?.ToList();
        }
    }
}