using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface ILabelQueryService
    {
        Task<List<LabelQuery>> Get();
        Task<LabelQuery> Get(long id);
        Task Create(LabelQuery label);
        Task Update(LabelQuery labelIn);
        Task Remove(long id);
        Task<List<LabelQuery>> GetByPredicate(Expression<Func<LabelQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<LabelQuery>> GetByPredicate(Expression<Func<LabelQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<LabelQuery, bool>> predicate);
    }
    public class LabelQueryService : ILabelQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IRequestTargetQueryService _requestTargetQueryService;
        public LabelQueryService(IMongoRepository repository, IRequestTargetQueryService requestTargetQueryService)
        {
            _repository = repository;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public Task<List<LabelQuery>> Get()
        {
            return _repository.LabelQueryCollection.Find(label => true).ToListAsync();
        }

        public Task<LabelQuery> Get(long id)
        {
            return _repository.LabelQueryCollection.Find(label => label.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(LabelQuery label)
        {
            try
            {
                var eventualConsistensy = await Get(label.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                if (label.ParentId != null)
                {
                    var directParent = await Get(label.ParentId.Value);
                    directParent.HasChild = true;
                    await _repository.LabelQueryCollection.ReplaceOneAsync(m => m.Id == directParent.Id, directParent);
                }
                await _repository.LabelQueryCollection.InsertOneAsync(label);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(LabelQuery labelIn)
        {
            var eventualConsistensy = await Get(labelIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }

            if (eventualConsistensy.LastEventPublishDate > labelIn.LastEventPublishDate)
            {
                return;
            }

            var savedlabel = eventualConsistensy;
            savedlabel.Title = labelIn.Title;
            savedlabel.LastEventPublishDate = labelIn.LastEventPublishDate;
            savedlabel.LastEventId = labelIn.LastEventId;

            await _repository.LabelQueryCollection.ReplaceOneAsync(m => m.Id == savedlabel.Id, savedlabel);
            await RequestTargetUpdateLabel(labelIn);
        }

        public async Task RequestTargetUpdateLabel(LabelQuery labelIn)
        {
            var filterbuildRequestTarget = Builders<RequestTargetQuery>.Filter;
            var filterRqAct = filterbuildRequestTarget.ElemMatch(doc => doc.LabelList, el => el.Id == labelIn.Id);

            var requestTargetQueries = await _repository.RequestTargetQueryCollection.Find(filterRqAct).ToListAsync();

            foreach (var item in requestTargetQueries)
            {
                item.LabelList = item.LabelList.Select(m =>
                {
                    if (m.Id == labelIn.Id)
                    {
                        m.Title = labelIn.Title;
                    }
                    return m;
                }).ToList();

                await _requestTargetQueryService.Update(item);
            }
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                if (saved.ParentId != null)
                {
                    var parent = await Get(saved.ParentId.Value);
                    parent.HasChild = false;
                    await _repository.LabelQueryCollection.ReplaceOneAsync(label => label.Id == parent.Id, parent);
                }
                await _repository.LabelQueryCollection.DeleteOneAsync(label => label.Id == id);
            }
        }

        public Task<List<LabelQuery>> GetByPredicate(Expression<Func<LabelQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.LabelQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<LabelQuery>> GetByPredicate(Expression<Func<LabelQuery, bool>> predicate)
        {
            return _repository.LabelQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<LabelQuery, bool>> predicate)
        {
            return _repository.LabelQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }
    }
}