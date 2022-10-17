using System.Collections.Generic;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Contract
{
    public class PointFilterContract : PagingContract
    {
        // public long? ParentId { get; set; }
        public string Title { get; set; }
        public bool? ShowCity { get; set; }
        public bool? ShowParent { get; set; }

        public List<SearchParameter> GetSearchParameters()
        {
            var searchParameters = new List<SearchParameter>();
            if (this.ShowCity == true)
            {
                searchParameters.Add(new SearchParameter() { Value = false, Filed = "HasChild", SearchType = SearchType.EqualValue });
            }

            else if (this.ShowParent == true)
            {
                searchParameters.Add(new SearchParameter() { Value = null, Filed = "ParentId", SearchType = SearchType.EqualValue });
            }

            if (!string.IsNullOrEmpty(this.Title))
            {
                searchParameters.Add(new SearchParameter() { Value = this.Title, Filed = "Title", SearchType = SearchType.ContainText });
            }
            return searchParameters;
        }

    }
}