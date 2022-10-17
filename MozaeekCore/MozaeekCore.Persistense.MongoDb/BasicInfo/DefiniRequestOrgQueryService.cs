using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IDefiniteRequestOrgQueryService
    {
        Task<List<DefiniteRequestOrgQuery>> Get();
        Task<DefiniteRequestOrgQuery> Get(long id);
        Task<List<DefiniteRequestOrgQuery>> GetByIds(List<long> ids);
        Task Create(DefiniteRequestOrgQueryParameter requestOrg);
        Task Update(DefiniteRequestOrgQueryParameter parameter);
        Task Remove(long id);
        Task<List<DefiniteRequestOrgQuery>> GetByPredicate(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<DefiniteRequestOrgQuery>> GetByPredicate(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate);
    }

    public class DefiniteRequestOrgQueryService : IDefiniteRequestOrgQueryService
    {
        private readonly IMongoRepository _repository;

        public DefiniteRequestOrgQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<DefiniteRequestOrgQuery>> Get()
        {
            return _repository.DefiniteRequestOrgQueryCollection.Find(requestOrg => true).ToListAsync();
        }

        public Task<DefiniteRequestOrgQuery> Get(long id)
        {
            return _repository.DefiniteRequestOrgQueryCollection.Find(requestOrg => requestOrg.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(DefiniteRequestOrgQueryParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                var point =await _repository.PointQueryCollection.Find(m => m.Id == parameter.Point).FirstOrDefaultAsync();
                var requestOrg = await _repository.RequestOrgQueryCollection.Find(m => m.Id == parameter.RequestOrg).FirstOrDefaultAsync();

                var query = new DefiniteRequestOrgQuery(parameter.Id, requestOrg, point, parameter.Address,
                    parameter.PhoneNumber, parameter.LastEventPublishDate, parameter.LastEventId);
                await _repository.DefiniteRequestOrgQueryCollection.InsertOneAsync(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(DefiniteRequestOrgQueryParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.LastEventPublishDate)
            {
                return;
            }
            var query = eventualConsistensy;
            var point = await _repository.PointQueryCollection.Find(m => m.Id == parameter.Point).FirstOrDefaultAsync();

            query.Update(point, parameter.Address,parameter.PhoneNumber,parameter.LastEventPublishDate,parameter.LastEventId);

            await _repository.DefiniteRequestOrgQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.DefiniteRequestOrgQueryCollection.DeleteOneAsync(requestOrg => requestOrg.Id == id);
            }
        }

        public Task<List<DefiniteRequestOrgQuery>> GetByPredicate(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.DefiniteRequestOrgQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<DefiniteRequestOrgQuery>> GetByPredicate(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate)
        {
            return _repository.DefiniteRequestOrgQueryCollection
                .Find(predicate).ToListAsync();

        }

        public Task<long> GetCount(Expression<Func<DefiniteRequestOrgQuery, bool>> predicate)
        {
            return _repository.DefiniteRequestOrgQueryCollection
                .Find(predicate).CountDocumentsAsync();

        }

        public async Task<List<DefiniteRequestOrgQuery>> GetByIds(List<long> ids)
        {
            var result = new List<DefiniteRequestOrgQuery>();
            var builder = Builders<DefiniteRequestOrgQuery>.Filter;
            if (ids.Any())
            {
                var list = builder.In("Id", ids);
                result.AddRange(await _repository.DefiniteRequestOrgQueryCollection.Find(list).ToListAsync());
            }
            return result;
        }
    }
}