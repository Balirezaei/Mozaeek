using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestListGroupByRequestTargetQueryHandler : IBaseAsyncQueryHandler<RequestPagingContract, List<RequestListGroupByRequestTarget>>
    {
        private readonly IRequestQueryService _requestQueryService;

        public GetRequestListGroupByRequestTargetQueryHandler(IRequestQueryService requestQueryService)
        {
            _requestQueryService = requestQueryService;
        }

        public async Task<List<RequestListGroupByRequestTarget>> HandleAsync(RequestPagingContract query)
        {
            var request =
                await _requestQueryService.GetByPredicate(new PagingQueryModelContract(query.PageSize, query.PageNumber, query.Sort, query.Order, query.GetSearchParameters()));

            return request.GroupBy(m => m.RequestTarget.Id).Select(m => new RequestListGroupByRequestTarget()
            {
                Title = m.First().RequestTarget.Title,
                Id = m.First().Id,
                RequestActId = m.First().RequestAct.Id,
                Description = m.First().Description,
                RequestActs = m.Select(z => new RequestActDto()
                {
                    Id = z.RequestAct.Id,
                    Title = z.RequestAct.Title
                }).ToList(),
                Points = m.SelectMany(n=>n.Points).Select(z=>new PointDto(){Id = z.Id,ParentId = z.ParentId,Title = z.Title}).ToList(),
                RequestNecessities = m.First().Necessities.Select(z => new RequestNecessityDto(z.Id, z.Description, z.Priority)).ToList(),
                RequestActions = m.First().Actions.Select(z => new RequestActionDto(z.Id, z.Description, z.Priority)).ToList(),
                RequestQualifications = m.First().Qualifications.Select(z => new RequestQualificationDto(z.Id, z.Description, z.Priority)).ToList(),
            }).ToList();
        }
    }
}