using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface ISynonymQueryService
    {
        Task<List<SynonymsQuery>> Get();
        Task<SynonymsQuery> Get(long id);
        Task Create(SynonymsQuery synonyms);
        Task Remove(long id);
        Task<List<SynonymsQuery>> GetByPredicate(Expression<Func<SynonymsQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<SynonymsQuery>> GetByPredicate(Expression<Func<SynonymsQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<SynonymsQuery, bool>> predicate);
        Task<long> GetNextId();
    }

    public class SynonymQueryService : ISynonymQueryService
    {
        private readonly IMongoRepository _repository;
        public SynonymQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<SynonymsQuery>> Get()
        {
            return _repository.SynonymQueryCollection.Find(Synonym => true).ToListAsync();
        }

        public Task<SynonymsQuery> Get(long id)
        {
            return _repository.SynonymQueryCollection.Find(Synonym => Synonym.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(SynonymsQuery synonyms)
        {
            try
            {
                var eventualConsistensy = await Get(synonyms.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                await _repository.SynonymQueryCollection.InsertOneAsync(synonyms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.SynonymQueryCollection.DeleteOneAsync(Synonym => Synonym.Id == id);
            }
        }

        public Task<List<SynonymsQuery>> GetByPredicate(Expression<Func<SynonymsQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.SynonymQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<SynonymsQuery>> GetByPredicate(Expression<Func<SynonymsQuery, bool>> predicate)
        {
            return _repository.SynonymQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<SynonymsQuery, bool>> predicate)
        {
            return _repository.SynonymQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public async Task<long> GetNextId()
        {
            var lastInserted = await _repository.SynonymQueryCollection.Find(x => true).SortByDescending(d => d.Id).Limit(1).FirstOrDefaultAsync();
            if (lastInserted!=null)
            {
                return (lastInserted.Id + 1);
            }

            return 1;
        }
    }
}