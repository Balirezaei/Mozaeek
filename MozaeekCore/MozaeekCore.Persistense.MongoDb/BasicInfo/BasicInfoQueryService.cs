using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IBasicInfoQueryService
    {
        Task<List<LabelQuery>> GetLabelChildrenByParentIds(List<long> ids);
        Task<List<LabelQuery>> GetLabelByIds(List<long> ids);
        Task<List<SubjectQuery>> GetSubjectChildrenByParentIds(List<long> ids);
        Task<List<SubjectQuery>> GetSubjectByIds(List<long> ids);
        Task<List<RequestOrgQuery>> GetRequestOrgChildrenByParentIds(List<long> ids);
        Task<List<RequestOrgQuery>> GetRequestOrgByIds(List<long> ids);
        Task<List<DefiniteRequestOrgQuery>> GetDefiniteRequestOrgByIds(List<long> ids);

        Task<List<PointQuery>> GetPointChildrenByParentIds(List<long> ids);
        Task<List<PointQuery>> GetPointByIds(List<long> ids);
        Task<List<ConnectedRequestTarget>> GetRequestTargetByIds(List<ConnectedRequestTarget> targets);
    }
    public class BasicInfoQueryService : IBasicInfoQueryService
    {
        private readonly IMongoRepository _repository;

        public BasicInfoQueryService(IMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LabelQuery>> GetLabelChildrenByParentIds(List<long> ids)
        {
            var res = new List<LabelQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }
            foreach (var id in ids)
            {
                res.AddRange(await GetLabelAllChildren(id));
            }
            return res;
        }

        public async Task<List<LabelQuery>> GetLabelByIds(List<long> ids)
        {
            var res = new List<LabelQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }
            var filterList = new List<FilterDefinition<LabelQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<LabelQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<LabelQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.LabelQueryCollection.FindAsync(orFilter)).ToList());
            return res;
        }

        public async Task<List<LabelQuery>> GetLabelAllChildren(long id)
        {
            var res = new List<LabelQuery>();
            var children = await _repository.LabelQueryCollection.Find(m => m.ParentId == id).ToListAsync();
            if (children.Any())
            {
                res.AddRange(children);
                foreach (var child in children)
                {
                    res.AddRange(await GetLabelAllChildren(child.Id));
                }
            }
            return res;
        }


        public async Task<List<SubjectQuery>> GetSubjectAllChildren(long id)
        {
            var res = new List<SubjectQuery>();
            var children = await _repository.SubjectQueryCollection.Find(m => m.ParentId == id).ToListAsync();
            if (children.Any())
            {
                res.AddRange(children);
                foreach (var child in children)
                {
                    res.AddRange(await GetSubjectAllChildren(child.Id));
                }
            }
            return res;
        }

        public async Task<List<SubjectQuery>> GetSubjectChildrenByParentIds(List<long> ids)
        {
            var res = new List<SubjectQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }
            foreach (var id in ids)
            {
                res.AddRange(await GetSubjectAllChildren(id));
            }
            return res;
        }

        public async Task<List<SubjectQuery>> GetSubjectByIds(List<long> ids)
        {
            var res = new List<SubjectQuery>();
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

            res.AddRange((await _repository.SubjectQueryCollection.FindAsync(orFilter)).ToList());
            return res;
        }
        #region RequestOrg
        public async Task<List<RequestOrgQuery>> GetRequestOrgAllChildren(long id)
        {
            var res = new List<RequestOrgQuery>();
            var children = await _repository.RequestOrgQueryCollection.Find(m => m.ParentId == id).ToListAsync();
            if (children.Any())
            {
                res.AddRange(children);
                foreach (var child in children)
                {
                    res.AddRange(await GetRequestOrgAllChildren(child.Id));
                }
            }
            return res;
        }

        public async Task<List<RequestOrgQuery>> GetRequestOrgChildrenByParentIds(List<long> ids)
        {
            var res = new List<RequestOrgQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }
            foreach (var id in ids)
            {
                res.AddRange(await GetRequestOrgAllChildren(id));
            }
            return res;
        }

        public async Task<List<RequestOrgQuery>> GetRequestOrgByIds(List<long> ids)
        {
            var res = new List<RequestOrgQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }

            var filterList = new List<FilterDefinition<RequestOrgQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<RequestOrgQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<RequestOrgQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.RequestOrgQueryCollection.FindAsync(orFilter)).ToList());
            return res;
        }

        public async Task<List<DefiniteRequestOrgQuery>> GetDefiniteRequestOrgByIds(List<long> ids)
        {
            var res = new List<DefiniteRequestOrgQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }

            var filterList = new List<FilterDefinition<DefiniteRequestOrgQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<DefiniteRequestOrgQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<DefiniteRequestOrgQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.DefiniteRequestOrgQueryCollection.FindAsync(orFilter)).ToList());
            return res;
        }

        #endregion

        #region Point
        public async Task<List<PointQuery>> GetPointAllChildren(long id)
        {
            var res = new List<PointQuery>();
            var children = await _repository.PointQueryCollection.Find(m => m.ParentId == id).ToListAsync();
            if (children.Any())
            {
                res.AddRange(children);
                foreach (var child in children)
                {
                    res.AddRange(await GetPointAllChildren(child.Id));
                }
            }
            return res;
        }

        public async Task<List<PointQuery>> GetPointChildrenByParentIds(List<long> ids)
        {
            var res = new List<PointQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }
            foreach (var id in ids)
            {
                res.AddRange(await GetPointAllChildren(id));
            }
            return res;
        }

        public async Task<List<PointQuery>> GetPointByIds(List<long> ids)
        {
            var res = new List<PointQuery>();
            if (ids == null || ids.Count == 0)
            {
                return res;
            }

            var filterList = new List<FilterDefinition<PointQuery>>();
            foreach (var id in ids)
            {
                var filterBuilder = Builders<PointQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == id);
                filterList.Add(filter);
            }
            var orFilter = Builders<PointQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.PointQueryCollection.FindAsync(orFilter)).ToList());
            return res;
        }

        public async Task<List<ConnectedRequestTarget>> GetRequestTargetByIds(List<ConnectedRequestTarget> targets)
        {
            var res = new List<RequestTargetQuery>();
            if (targets == null || targets.Count == 0)
            {
                return new List<ConnectedRequestTarget>();
            }

            var filterList = new List<FilterDefinition<RequestTargetQuery>>();
            foreach (var target in targets)
            {
                var filterBuilder = Builders<RequestTargetQuery>.Filter;
                var filter = filterBuilder.Where(z => z.Id == target.RequestTargetId);
                filterList.Add(filter);
            }
            var orFilter = Builders<RequestTargetQuery>.Filter.Or(filterList);

            res.AddRange((await _repository.RequestTargetQueryCollection.FindAsync(orFilter)).ToList());

            return targets.Select(m =>
             {
                 var target = res.FirstOrDefault(z => z.Id == m.RequestTargetId);

                 m.Title = target.Title;
                 return m;
             }).ToList();
        }

        #endregion
    }
}