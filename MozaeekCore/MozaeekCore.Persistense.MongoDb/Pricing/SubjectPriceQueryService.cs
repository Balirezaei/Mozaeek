using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb.Pricing
{
    public interface ISubjectPriceQueryService
    {
        Task<List<SubjectPriceQuery>> Get();
        Task<SubjectPriceQuery> Get(long id);
        Task Create(SubjectPriceQuery subjectPriceQuery);
        Task Update(SubjectPriceQuery subjectPriceQuery);
        Task Remove(long id);
        Task<List<SubjectPriceQuery>> GetByPredicate(Expression<Func<SubjectPriceQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<SubjectPriceQuery>> GetByPredicate(Expression<Func<SubjectPriceQuery, bool>> predicate);
        Task<long> GetCount(Expression<Func<SubjectPriceQuery, bool>> predicate);
        Task<SubjectPriceQuery> GetProperSubjectPriceBySubjectId(long subjectId);
    }

    public class SubjectPriceQueryService : ISubjectPriceQueryService
    {
        private readonly IMongoRepository _repository;

        public SubjectPriceQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<SubjectPriceQuery>> Get()
        {
            return _repository.SubjectPriceQueryCollection.Find(label => true).ToListAsync();
        }

        public Task<SubjectPriceQuery> Get(long id)
        {
            return _repository.SubjectPriceQueryCollection.Find(label => label.Id == id).FirstOrDefaultAsync();
        }
        public async Task Create(SubjectPriceQuery subjectPriceQuery)
        {
            try
            {
                var eventualConsistensy = await Get(subjectPriceQuery.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                var details = await
                    GetSubjectByIds(subjectPriceQuery
                        .SubjectPriceDetails
                        .Select(m => m.SubjectId).ToList());

                subjectPriceQuery.SubjectPriceDetails = details;

                await _repository.SubjectPriceQueryCollection.InsertOneAsync(subjectPriceQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(SubjectPriceQuery subjectPriceQuery)
        {
            var eventualConsistensy = await Get(subjectPriceQuery.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            var query = eventualConsistensy;
            query.Title = subjectPriceQuery.Title;
            query.StartDate = subjectPriceQuery.StartDate;
            query.SystemShare = subjectPriceQuery.SystemShare;
            query.TechnicianShare = subjectPriceQuery.TechnicianShare;
            query.IsActive = subjectPriceQuery.IsActive;
            query.EndDate = subjectPriceQuery.EndDate;
            query.PriceAmount = subjectPriceQuery.PriceAmount;
            query.PriceCurrencyId = subjectPriceQuery.PriceCurrencyId;
            query.PriceCurrencyTitle = subjectPriceQuery.PriceCurrencyTitle;
            var details = await
                GetSubjectByIds(subjectPriceQuery
                    .SubjectPriceDetails
                    .Select(m => m.SubjectId).ToList());

            query.SubjectPriceDetails = details;
            query.LastEventPublishDate = subjectPriceQuery.LastEventPublishDate;
            query.LastEventId = subjectPriceQuery.LastEventId;
            await _repository.SubjectPriceQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
        }

        public async Task<List<SubjectPriceDetailQuery>> GetSubjectByIds(List<long> ids)
        {
            var res = new List<SubjectPriceDetailQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }

            var filterList = new List<FilterDefinition<SubjectQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<SubjectQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<SubjectQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.SubjectQueryCollection.FindAsync(orFilter)).ToList().Select(m => new SubjectPriceDetailQuery()
            {
                SubjectId = m.Id,
                SubjectTitle = m.Title
            }));

            return res;
        }
        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.SubjectPriceQueryCollection.DeleteOneAsync(label => label.Id == id);
            }
        }

        public Task<List<SubjectPriceQuery>> GetByPredicate(Expression<Func<SubjectPriceQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.SubjectPriceQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<SubjectPriceQuery>> GetByPredicate(Expression<Func<SubjectPriceQuery, bool>> predicate)
        {
            return _repository.SubjectPriceQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<SubjectPriceQuery, bool>> predicate)
        {
            return _repository.SubjectPriceQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }

        public Task<SubjectPriceQuery> GetProperSubjectPriceBySubjectId(long subjectId)
        {
            var filterBuilder = Builders<SubjectPriceQuery>.Filter;

            var f1 = filterBuilder.ElemMatch(doc => doc.SubjectPriceDetails, el => el.SubjectId == subjectId);
            var f2 = filterBuilder.Eq(m => m.IsActive, true);
            //var f3 = filterBuilder.Lte(m => m.EndDate, DateTime.Now);
            var filterConcat = filterBuilder.And(new[]{
                f1,f2//,f3
            });
            return _repository.SubjectPriceQueryCollection.Find(filterConcat).SingleOrDefaultAsync();
        }
    }
}