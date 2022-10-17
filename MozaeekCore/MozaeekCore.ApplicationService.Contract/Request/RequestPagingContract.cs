using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestPagingContract : PagingContract
    {
        public string Title { get; set; }
        public long[] ExcludeRequestIds { get; set; }
        public long? RequestTargetId { get; set; }
        public long? RequestActId { get; set; }
        public long? PointId { get; set; }

        public List<SearchParameter> GetSearchParameters()
        {
            var searchParameters = new List<SearchParameter>();
            if (!string.IsNullOrEmpty(this.Title))
            {
                searchParameters.Add(new SearchParameter() { Value = this.Title, Filed = "Title", SearchType = SearchType.ContainText });
            }
            if (this.ExcludeRequestIds != null)
            {
                foreach (var id in this.ExcludeRequestIds)
                {
                    searchParameters.Add(new SearchParameter() { Value = id, Filed = "Id", SearchType = SearchType.NotEqualValue });
                }
            }

            if (RequestTargetId.HasValue && RequestTargetId != 0)
            {
                searchParameters.Add(new SearchParameter() { Value = this.RequestTargetId, Filed = "RequestTarget.Id", SearchType = SearchType.EqualValue });
            }


            if (RequestActId.HasValue && RequestActId != 0)
            {
                searchParameters.Add(new SearchParameter() { Value = this.RequestActId, Filed = "RequestAct.Id", SearchType = SearchType.EqualValue });
            }

            if (PointId.HasValue && PointId != 0)
            {
                searchParameters.Add(new SearchParameter() { Value = this.PointId, Filed = "Points.Id", SearchType = SearchType.EqualValue });
            }

            return searchParameters;
        }
    }
}