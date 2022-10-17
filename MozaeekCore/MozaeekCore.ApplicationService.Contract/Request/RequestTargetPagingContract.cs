using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTargetPagingContract : PagingContract
    {
        public string Title { get; set; }
        public long[] ExcludeRequestTargetIds { get; set; }
        public bool? ShowIsDocument { get; set; }
        public List<SearchParameter> GetSearchParameters()
        {
            var searchParameters = new List<SearchParameter>();
            if (!string.IsNullOrEmpty(this.Title))
            {
                searchParameters.Add(new SearchParameter() { Value = this.Title, Filed = "Title", SearchType = SearchType.ContainText });
            }
            if (this.ExcludeRequestTargetIds != null)
            {
                foreach (var id in this.ExcludeRequestTargetIds)
                {
                    searchParameters.Add(new SearchParameter() { Value = id, Filed = "Id", SearchType = SearchType.NotEqualValue });
                }
            }
            if (ShowIsDocument.HasValue)
            {
                searchParameters.Add(new SearchParameter() { Value = ShowIsDocument.Value, Filed = "IsDocument", SearchType = SearchType.EqualValue });
            }
            return searchParameters;
        }
    }
}