using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb.Pricing
{
    public interface IRequestPriceQueryService
    {
        Task<List<RequestPriceQuery>> Get();
        Task<RequestPriceQuery> Get(long id);
        Task Create(RequestPriceQuery requestPriceQuery);
        Task Update(RequestPriceQuery requestPriceQuery);
        Task Remove(long id);
        Task<List<RequestPriceQuery>> GetByPredicate(Expression<Func<RequestPriceQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<RequestPriceQuery>> GetByPredicate(Expression<Func<RequestPriceQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<RequestPriceQuery, bool>> predicate);
        Task<RequestPriceQuery> GetProperRequestPriceByRequestId(long requestId);
    }

    public class RequestPriceQueryService : IRequestPriceQueryService
    {
        private readonly IMongoRepository _repository;

        public RequestPriceQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<RequestPriceQuery>> Get()
        {
            return _repository.RequestPriceQueryCollection.Find(label => true).ToListAsync();
        }

        public Task<RequestPriceQuery> Get(long id)
        {
            return _repository.RequestPriceQueryCollection.Find(label => label.Id == id).FirstOrDefaultAsync();
        }
        public async Task Create(RequestPriceQuery requestPriceQuery)
        {
            try
            {
                var eventualConsistensy = await Get(requestPriceQuery.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                var details = await
                    GetRequestByIds(requestPriceQuery
                    .RequestPriceDetails
                    .Select(m => m.RequestId).ToList());

                requestPriceQuery.RequestPriceDetails = details;
                await _repository.RequestPriceQueryCollection.InsertOneAsync(requestPriceQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(RequestPriceQuery requestPriceQuery)
        {
            var eventualConsistensy = await Get(requestPriceQuery.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            var query = eventualConsistensy;
            query.Title = requestPriceQuery.Title;
            query.StartDate = requestPriceQuery.StartDate;
            query.SystemShare = requestPriceQuery.SystemShare;
            query.TechnicianShare = requestPriceQuery.TechnicianShare;
            query.IsActive = requestPriceQuery.IsActive;
            query.EndDate = requestPriceQuery.EndDate;
            query.PriceAmount = requestPriceQuery.PriceAmount;
            query.PriceCurrencyId = requestPriceQuery.PriceCurrencyId;
            query.PriceCurrencyTitle = requestPriceQuery.PriceCurrencyTitle;
            query.LastEventPublishDate = requestPriceQuery.LastEventPublishDate;
            query.LastEventId = requestPriceQuery.LastEventId;
            var details = await
                GetRequestByIds(requestPriceQuery
                    .RequestPriceDetails
                    .Select(m => m.RequestId).ToList());

            query.RequestPriceDetails = details;

            requestPriceQuery.RequestPriceDetails = details;

            await _repository.RequestPriceQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }
        public async Task<List<RequestPriceDetailQuery>> GetRequestByIds(List<long> ids)
        {
            var res = new List<RequestPriceDetailQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }

            var filterList = new List<FilterDefinition<RequestQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<RequestQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<RequestQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.RequestQueryCollection.FindAsync(orFilter)).ToList().Select(m => new RequestPriceDetailQuery()
            {
                RequestId = m.Id,
                RequestTitle = m.Title,
                FullOnline = m.FullOnline
            }));

            return res;
        }
        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.RequestPriceQueryCollection.DeleteOneAsync(label => label.Id == id);
            }
        }

        public Task<List<RequestPriceQuery>> GetByPredicate(Expression<Func<RequestPriceQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.RequestPriceQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestPriceQuery>> GetByPredicate(Expression<Func<RequestPriceQuery, bool>> predicate)
        {
            return _repository.RequestPriceQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<RequestPriceQuery, bool>> predicate)
        {
            return _repository.RequestPriceQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public Task<RequestPriceQuery> GetProperRequestPriceByRequestId(long requestId)
        {
            var filterBuilder = Builders<RequestPriceQuery>.Filter;

            var f1 = filterBuilder.ElemMatch(doc => doc.RequestPriceDetails, el => el.RequestId == requestId);
            var f2 = filterBuilder.Eq(m => m.IsActive, true);
            var f3 = filterBuilder.Gte(m => m.EndDate, DateTime.Now);

            var filterConcat = filterBuilder.And(new[]{
                f1,f2,f3
            });

            return _repository.RequestPriceQueryCollection.Find(filterConcat).SingleOrDefaultAsync();

        }
    }
}