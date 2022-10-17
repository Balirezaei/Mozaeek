using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MozaeekCore.Persistense.MongoDb.Tools;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IPointQueryService
    {
        Task<List<PointQuery>> Get();
        Task<PointQuery> Get(long id);
        Task Create(PointQuery point);
        Task Update(PointQuery pointIn);
        Task Remove(long id);
        Task RemoveAll();
        Task<List<PointQuery>> GetByPredicate(PagingQueryModelContract pagingQuerContract);
        Task<List<PointQuery>> GetByPredicate(Expression<Func<PointQuery, bool>> predicate);
        Task<long> GetCount(List<SearchParameter> parameters);
        Task<List<PointQuery>> GetAllLevelChildren(List<long> ids);
        Task<List<PointQuery>> GetAllByText(string query);
    }
    public class PointQueryService : IPointQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IRequestQueryService _requestQueryService;
        private readonly IAnnouncementQueryService _announcementQueryService;
        public PointQueryService(IMongoRepository repository, IRequestQueryService requestQueryService, IAnnouncementQueryService announcementQueryService)
        {
            _repository = repository;
            _requestQueryService = requestQueryService;
            _announcementQueryService = announcementQueryService;
        }

        public Task<List<PointQuery>> Get()
        {
            return _repository.PointQueryCollection.Find(point => true).ToListAsync();
        }

        public Task<PointQuery> Get(long id)
        {
            return _repository.PointQueryCollection.Find(point => point.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(PointQuery point)
        {
            var eventualConsistensy = await Get(point.Id);
            if (eventualConsistensy != null)
            {
                return;
            }
            if (point.ParentId != null)
            {
                var directParent = await Get(point.ParentId.Value);
                directParent.HasChild = true;
                await _repository.PointQueryCollection.ReplaceOneAsync(m => m.Id == directParent.Id, directParent);
            }
            await _repository.PointQueryCollection.InsertOneAsync(point);
        }

        public async Task Update(PointQuery pointIn)
        {
            #region eventualConsistensy
            var eventualConsistensy = await Get(pointIn.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > pointIn.LastEventPublishDate)
            {
                Console.WriteLine($"eventualConsistensy update not effected :{eventualConsistensy.LastEventPublishDate} --- {pointIn.LastEventPublishDate}");
                return;
            }
            #endregion

            var query = eventualConsistensy;
            query.Title = pointIn.Title;
            query.LastEventPublishDate = pointIn.LastEventPublishDate;
            query.LastEventId = pointIn.LastEventId;
            await _repository.PointQueryCollection.ReplaceOneAsync(m => m.Id == query.Id, query);

            //TODO: Update All Request With this Point
            //TODO: Update All Announcement With this Point
            await _requestQueryService.UpdateWithPoint(query);
            await _announcementQueryService.UpdateWithPoint(query);
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
                    await _repository.PointQueryCollection.ReplaceOneAsync(point => point.Id == parent.Id, parent);
                }
                await _repository.PointQueryCollection.DeleteOneAsync(point => point.Id == id);
     
            }
        }

        public Task<List<PointQuery>> GetByPredicate(PagingQueryModelContract pagingQuerContract)
        {
            var filters = Builders<PointQuery>.Filter.GenerateFilter(pagingQuerContract.SearchParameters );
            return _repository.PointQueryCollection
                .Find(filters)
                .Skip((pagingQuerContract.PageNumber - 1) * pagingQuerContract.PageSize)
                .Limit(pagingQuerContract.PageSize)
                .ToListAsync();
        }

        public Task<List<PointQuery>> GetByPredicate(Expression<Func<PointQuery, bool>> predicate)
        {
            return _repository.PointQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(List<SearchParameter> parameters)
        {
            var filters = Builders<PointQuery>.Filter.GenerateFilter(parameters);
            return _repository.PointQueryCollection
                .Find(filters).CountDocumentsAsync();
        }
        public async Task<List<PointQuery>> GetAllLevelChildren(List<long> ids)
        {
            var result = new List<PointQuery>();
            var builder = Builders<PointQuery>.Filter;
            var filter = builder.In("ParentId", ids);
            var children = _repository.PointQueryCollection.Find(filter)?.ToList();
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
        public async Task<List<PointQuery>> GetAllByText(string query)
        {
            var result = new List<PointQuery>();
            var builder = Builders<PointQuery>.Filter;
            var filter = builder.Regex("Title", BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
            return (await _repository.PointQueryCollection.FindAsync(filter))?.ToList();
        }

        public async Task RemoveAll()
        {
            await _repository.PointQueryCollection.DeleteManyAsync(point =>true);
         
        }
    }
}