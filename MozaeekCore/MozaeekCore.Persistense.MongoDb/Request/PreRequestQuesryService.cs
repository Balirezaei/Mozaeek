using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IPreRequestQueryService
    {
        Task<List<PreRequestQuery>> Get();
        Task<PreRequestQuery> Get(long id);
        Task Create(PreRequestQuery preRequest);
        Task Update(PreRequestQuery preRequestIn);
        Task Remove(long id);
        Task<List<PreRequestQuery>> GetByPredicate(Expression<Func<PreRequestQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<PreRequestQuery>> GetByPredicate(Expression<Func<PreRequestQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<PreRequestQuery, bool>> predicate);

    }
    public class PreRequestQueryService : IPreRequestQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IRequestTargetQueryService _requestTargetQueryService;
        public PreRequestQueryService(IMongoRepository repository, IRequestTargetQueryService requestTargetQueryService)
        {
            _repository = repository;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public Task<List<PreRequestQuery>> Get()
        {
            return _repository.PreRequestQueryCollection.Find(unProcessedRequest => true).ToListAsync();
        }

        public Task<PreRequestQuery> Get(long id)
        {
            return _repository.PreRequestQueryCollection.Find(unProcessedRequest => unProcessedRequest.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(PreRequestQuery preRequest)
        {
            try
            {
                var eventualConsistensy = await Get(preRequest.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                await _repository.PreRequestQueryCollection.InsertOneAsync(preRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(PreRequestQuery preRequestIn)
        {
            var eventualConsistensy = await Get(preRequestIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > preRequestIn.LastEventPublishDate)
            {
                return;
            }
            var savedunProcessedRequest = eventualConsistensy;
            savedunProcessedRequest.Title = preRequestIn.Title;
            savedunProcessedRequest.Summery = preRequestIn.Summery;
            savedunProcessedRequest.IsProcessed = preRequestIn.IsProcessed;

            savedunProcessedRequest.LastEventPublishDate = preRequestIn.LastEventPublishDate;
            savedunProcessedRequest.LastEventId = preRequestIn.LastEventId;

            await _repository.PreRequestQueryCollection.ReplaceOneAsync(m => m.Id == savedunProcessedRequest.Id, savedunProcessedRequest);

        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.PreRequestQueryCollection.DeleteOneAsync(unProcessedRequest => unProcessedRequest.Id == id);
            }
        }

        public Task<List<PreRequestQuery>> GetByPredicate(Expression<Func<PreRequestQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.PreRequestQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<PreRequestQuery>> GetByPredicate(Expression<Func<PreRequestQuery, bool>> predicate)
        {
            return _repository.PreRequestQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<PreRequestQuery, bool>> predicate)
        {
            return _repository.PreRequestQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }



    }
}