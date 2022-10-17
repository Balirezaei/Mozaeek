using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPointsCountQueryHandler : IBaseAsyncQueryHandler<PointFilterContract, PointTotalCount>
    {
        private readonly IPointQueryService _labelQueryService;


        public GetPointsCountQueryHandler(IPointQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<PointTotalCount> HandleAsync(PointFilterContract query)
        {
            var searchParameters = new List<SearchParameter>();

            if (!string.IsNullOrEmpty(query.Title))
            {
                searchParameters.Add(new SearchParameter() { Value = query.Title, Filed = "Title", SearchType = SearchType.ContainText });
            }

            if (query.ShowCity != null)
            {
                searchParameters.Add(new SearchParameter() { Value = query.ShowCity.Value, Filed = "HasChild", SearchType = SearchType.EqualValue });
            }

            long count = await _labelQueryService.GetCount(searchParameters);
            return new PointTotalCount(count);
        }
    }
}