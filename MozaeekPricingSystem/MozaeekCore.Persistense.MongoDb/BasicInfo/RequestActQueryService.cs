using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IRequestActQueryService
    {
        Task<List<RequestActQuery>> Get();
        Task<RequestActQuery> Get(long id);
        Task Create(RequestActQuery requestAct);
        Task Update(RequestActQuery requestActIn);
        Task Remove(long id);
        Task<List<RequestActQuery>> GetByPredicate(Expression<Func<RequestActQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<RequestActQuery>> GetByPredicate(Expression<Func<RequestActQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<RequestActQuery, bool>> predicate);
    }
    public class RequestActQueryService : IRequestActQueryService
    {
        private readonly IMongoRepository _repository;

        public RequestActQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<RequestActQuery>> Get()
        {
            return _repository.RequestActQueryCollection.Find(actQuery => true).ToListAsync();
        }

        public Task<RequestActQuery> Get(long id)
        {
            return _repository.RequestActQueryCollection.Find(actQuery => actQuery.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(RequestActQuery requestAct)
        {
            try
            {
                var eventualConsistensy = await Get(requestAct.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                await _repository.RequestActQueryCollection.InsertOneAsync(requestAct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(RequestActQuery requestActIn)
        {
            var eventualConsistensy = await Get(requestActIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }

            if (eventualConsistensy.LastEventPublishDate > requestActIn.LastEventPublishDate)
            {
                return;
            }

            var query = eventualConsistensy;
            query.Title = requestActIn.Title;
            query.LastEventPublishDate = requestActIn.LastEventPublishDate;
            query.LastEventId = requestActIn.LastEventId;

           await _repository.RequestActQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.RequestActQueryCollection.DeleteOneAsync(actQuery => actQuery.Id == id);
            }
        }

        public Task<List<RequestActQuery>> GetByPredicate(Expression<Func<RequestActQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.RequestActQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestActQuery>> GetByPredicate(Expression<Func<RequestActQuery, bool>> predicate)
        {
            return _repository.RequestActQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<RequestActQuery, bool>> predicate)
        {
            return _repository.RequestActQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }
    }

}