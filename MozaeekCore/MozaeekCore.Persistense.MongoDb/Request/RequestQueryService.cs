using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekCore.Persistense.MongoDb.Tools;
using MozaeekCore.QueryModel;
using Newtonsoft.Json;

namespace MozaeekCore.Persistense.MongoDb
{
    public interface IRequestQueryService
    {
        Task<List<RequestQuery>> Get();
        Task<RequestQuery> Get(long id);
        Task Create(RequestParameter parameter);
        Task Update(RequestParameter parameter);
        Task Remove(long id);
        Task<List<RequestQuery>> GetByPredicate(PagingQueryModelContract pagingQuerContract);
        Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate);

        Task<long> GetCount(List<SearchParameter> parameters);
        Task UpdateWithPoint(PointQuery point);
    }
    public class RequestQueryService : IRequestQueryService
    {
        private readonly IMongoRepository _repository;
        private readonly IBasicInfoQueryService _basicInfoQueryService;

        public RequestQueryService(IMongoRepository repository, IBasicInfoQueryService basicInfoQueryService)
        {
            _repository = repository;
            _basicInfoQueryService = basicInfoQueryService;
        }

        public Task<List<RequestQuery>> Get()
        {
            return _repository.RequestQueryCollection.Find(request => true).ToListAsync();
        }

        public Task<RequestQuery> Get(long id)
        {
            return _repository.RequestQueryCollection.Find(request => request.Id == id).FirstOrDefaultAsync();
        }


        public async Task Create(RequestParameter parameter)
        {
            try
            {
                var eventualConsistensy = await Get(parameter.Id);
                if (eventualConsistensy != null)
                {
                    return;
                }
                var target = (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.TargetId)).FirstOrDefault();
                Console.WriteLine($"read target");

                var act = (await _repository.RequestActQueryCollection.FindAsync(m => m.Id == parameter.ActId))
                    .FirstOrDefault();
                Console.WriteLine($"read act");

                var pointList = await _basicInfoQueryService.GetPointByIds(parameter.PointIds);
                var requestOrgs = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgs);
                Console.WriteLine("DefiniteRequestOrgs " + JsonConvert.SerializeObject(parameter.DefiniteRequestOrgs));
                var definiteRequestOrgs = await _basicInfoQueryService.GetDefiniteRequestOrgByIds(parameter.DefiniteRequestOrgs);
                Console.WriteLine("DefiniteRequestOrgs " + JsonConvert.SerializeObject(definiteRequestOrgs));

                Console.WriteLine($"read pointList");

                //var pointsChildren = await _basicInfoQueryService.GetPointChildrenByParentIds(parameter.PointIds);
                //Console.WriteLine($"read pointsChildren");

                var title = act.Title + " " + target.Title;
                var targetsConnected = await _basicInfoQueryService.GetRequestTargetByIds(parameter.ConnectedRequestTargets);
                Console.WriteLine($"read targetsConnected");

                await _repository.RequestQueryCollection.InsertOneAsync(new RequestQuery(parameter.Id,
                  title, act, target, parameter.Necessities,
                  parameter.Actions, parameter.QualificationIds,
                  pointList, //pointsChildren,
                  requestOrgs,
                  definiteRequestOrgs,
                  targetsConnected,
                  parameter.PublishEventDate,
                  parameter.EventId, parameter.FullOnline,
                  parameter.Summary, parameter.Regulation, parameter.RequestExpiredDate, parameter.RequestStartDate));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"{ex.InnerException.Message}");
                }
                Console.WriteLine($"{ex.Message}");
                throw ex;
            }
        }

        public async Task Update(RequestParameter parameter)
        {
            var eventualConsistensy = await Get(parameter.Id);
            if (eventualConsistensy == null)
            {
                return;
            }
            if (eventualConsistensy.LastEventPublishDate > parameter.PublishEventDate)
            {
                return;
            }
            var obj = eventualConsistensy;
            var target = (await _repository.RequestTargetQueryCollection.FindAsync(m => m.Id == parameter.TargetId)).FirstOrDefault();
            var act = (await _repository.RequestActQueryCollection.FindAsync(m => m.Id == parameter.ActId))
                .FirstOrDefault();
            var title = act.Title + " " + target.Title;
            obj.Title = title;
            obj.RequestTarget = target;
            obj.RequestAct = act;
            obj.Actions = parameter.Actions;
            //obj.Documents = parameter.Documents;
            obj.Qualifications = parameter.QualificationIds;
            obj.FullOnline = parameter.FullOnline;
            obj.Necessities = parameter.Necessities;
            obj.Description = parameter.Summary;
            obj.Regulation = parameter.Regulation;
            obj.Points = await _basicInfoQueryService.GetPointByIds(parameter.PointIds);
            obj.RequestOrgs = await _basicInfoQueryService.GetRequestOrgByIds(parameter.RequestOrgs);
            obj.DefiniteRequestOrgs = await _basicInfoQueryService.GetDefiniteRequestOrgByIds(parameter.DefiniteRequestOrgs);
            //obj.PointsChildren = await _basicInfoQueryService.GetPointChildrenByParentIds(parameter.PointIds);
            var targetsConnected = await _basicInfoQueryService.GetRequestTargetByIds(parameter.ConnectedRequestTargets);
            obj.ConnectedRequestTargets = targetsConnected;

            obj.LastEventPublishDate = parameter.PublishEventDate;
            obj.RequestStartDate = parameter.RequestStartDate;
            obj.RequestExpiredDate = parameter.RequestExpiredDate;

            obj.LastEventId = parameter.EventId;
            await _repository.RequestQueryCollection.ReplaceOneAsync(m => m.Id == obj.Id, obj);
        }

        public async Task Remove(long id)
        {
            var saved = await Get(id);
            if (saved != null)
            {
                await _repository.RequestQueryCollection.DeleteOneAsync(request => request.Id == id);
            }
        }

        public Task<List<RequestQuery>> GetByPredicate(PagingQueryModelContract pagingQuerContract)
        {
            var filters = Builders<RequestQuery>.Filter.GenerateFilter(pagingQuerContract.SearchParameters);
            return _repository.RequestQueryCollection
                .Find(filters)
                .Skip((pagingQuerContract.PageNumber - 1) * pagingQuerContract.PageSize)
                .Limit(pagingQuerContract.PageSize)
                .ToListAsync();
        }

        public Task<List<RequestQuery>> GetByPredicate(Expression<Func<RequestQuery, bool>> predicate)
        {
            return _repository.RequestQueryCollection
                .Find(predicate).ToListAsync();
        }

        public Task<long> GetCount(List<SearchParameter> parameters)
        {
            var filters = Builders<RequestQuery>.Filter.GenerateFilter(parameters);
            return _repository.RequestQueryCollection
                .Find(filters).CountDocumentsAsync();
        }

        public async Task UpdateWithPoint(PointQuery point)
        {
            var filterbuildRequestTarget = Builders<RequestQuery>.Filter;
            var filterRqAct =
                filterbuildRequestTarget.ElemMatch(doc => doc.Points, el => el.Id == point.Id);

            var requestQueries = await _repository.RequestQueryCollection.Find(filterRqAct).ToListAsync();

            foreach (var item in requestQueries)
            {
                item.Points = item.Points.Select(m =>
                {
                    if (m.Id == point.Id)
                    {
                        m.Title = point.Title;
                    }
                    return m;
                }).ToList();
                item.Points = item.Points.Select(m =>
                {
                    if (m.Id == point.Id)
                    {
                        m.Title = point.Title;
                    }
                    return m;
                }).ToList();
                await _repository.RequestQueryCollection.ReplaceOneAsync(m => m.Id == item.Id, item);
            }
        }




    }

}