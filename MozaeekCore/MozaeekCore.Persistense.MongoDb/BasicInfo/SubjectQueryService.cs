using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MozaeekCore.Common;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface ISubjectQueryService
    {
        Task<List<SubjectQuery>> Get();
        Task<SubjectQuery> Get(long id);
        Task Create(SubjectQuery subject);
        Task Update(SubjectQuery subjectIn);
        Task Remove(long id);
        Task<List<SubjectQuery>> GetByPredicate(Expression<Func<SubjectQuery, bool>> predicate, PagingContract pagingContract);
        Task<List<SubjectQuery>> GetByPredicate(Expression<Func<SubjectQuery, bool>> predicate);

        Task<long> GetCount(Expression<Func<SubjectQuery, bool>> predicate);

        Task<List<SubjectQuery>> GetAllByText(string query);
        Task<List<SubjectQuery>> GetAllLevelChildrenLookUp(List<long> ids);

    }
    public class SubjectQueryService : ISubjectQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IRequestTargetQueryService _requestTargetQueryService;
        public SubjectQueryService(IMongoRepository repository, IRequestTargetQueryService requestTargetQueryService)
        {
            _repository = repository;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public Task<List<SubjectQuery>> Get()
        {
            return _repository.SubjectQueryCollection.Find(subject => true).ToListAsync();
        }

        public Task<SubjectQuery> Get(long id)
        {
            return _repository.SubjectQueryCollection.Find(subject => subject.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(SubjectQuery subject)
        {
            try
            {
                var eventualConsistensy = await Get(subject.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }

                if (subject.ParentId != null)
                {
                    var directParent = await Get(subject.ParentId.Value);
                    directParent.HasChild = true;
                    await _repository.SubjectQueryCollection.ReplaceOneAsync(m => m.Id == directParent.Id, directParent);
                }
                await _repository.SubjectQueryCollection.InsertOneAsync(subject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(SubjectQuery subjectIn)
        {
            var eventualConsistensy = await Get(subjectIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }

            if (eventualConsistensy.LastEventPublishDate > subjectIn.LastEventPublishDate)
            {
                return;
            }
            var query = eventualConsistensy;
            query.Title = subjectIn.Title;
            query.Icon = subjectIn.Icon;
            query.LastEventPublishDate = subjectIn.LastEventPublishDate;
            query.LastEventId = subjectIn.LastEventId;

            //TODO: Update All News With this Subject
            await _repository.SubjectQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);
            await RequestTargetUpdateSubject(query);
        }

        public async Task RequestTargetUpdateSubject(SubjectQuery subject)
        {
            var filterbuildRequestTarget = Builders<RequestTargetQuery>.Filter;
            var filterRqAct = filterbuildRequestTarget.ElemMatch(doc => doc.SubjectList, el => el.Id == subject.Id);

            var preSavedRequestTarget = await _repository.RequestTargetQueryCollection.Find(filterRqAct).ToListAsync();

            foreach (var item in preSavedRequestTarget)
            {
                item.SubjectList = item.SubjectList.Select(m =>
                {
                    if (m.Id == subject.Id)
                    {
                        m.Title = subject.Title;
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
                    var otherChildren =await GetByPredicate(m => m.ParentId == saved.ParentId && m.Id != id);

                    parent.HasChild = otherChildren.Any();
                    await _repository.SubjectQueryCollection.ReplaceOneAsync(subject => subject.Id == parent.Id, parent);
                }
                await _repository.SubjectQueryCollection.DeleteOneAsync(subject => subject.Id == id);
            }
        }

        public Task<List<SubjectQuery>> GetByPredicate(Expression<Func<SubjectQuery, bool>> predicate, PagingContract pagingContract)
        {
            return _repository.SubjectQueryCollection
                .Find(predicate)
                .Skip((pagingContract.PageNumber - 1) * pagingContract.PageSize)
                .Limit(pagingContract.PageSize)
                .ToListAsync();
        }

        public Task<List<SubjectQuery>> GetByPredicate(Expression<Func<SubjectQuery, bool>> predicate)
        {
            return _repository.SubjectQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(Expression<Func<SubjectQuery, bool>> predicate)
        {
            return _repository.SubjectQueryCollection
                .Find(predicate).CountDocumentsAsync();
        }
   
        public async Task<List<SubjectQuery>> GetAllByText(string query)
        {
            var result = new List<SubjectQuery>();
            var builder = Builders<SubjectQuery>.Filter;
            var filter = builder.Regex("Title", BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
             return _repository.SubjectQueryCollection.Find(filter)?.ToList();
        }

        public async Task<List<SubjectQuery>> GetAllLevelChildrenLookUp(List<long> ids)
        {
                var subject = _repository.SubjectQueryCollection.Aggregate();
                var connectFromField = (FieldDefinition<SubjectQuery, long?>)"Id";
                var connectToField = (FieldDefinition<SubjectQuery, long?>)"ParentId";
                var startWith = (AggregateExpressionDefinition<SubjectQuery, long?>)"$_id";
                var @as = (FieldDefinition<ParentChildHierarchyDto<SubjectQuery>, SubjectQuery[]>)"HierarchyResult";
                var result = await subject.Match(p => ids.Contains(p.Id)).GraphLookup(_repository.SubjectQueryCollection, connectFromField, connectToField, startWith, @as).ToListAsync();
                return result.First().HierarchyResult.ToList();
        }
    }
}