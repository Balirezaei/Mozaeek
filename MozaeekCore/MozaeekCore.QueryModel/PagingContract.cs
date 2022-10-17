using System.Collections.Generic;

namespace MozaeekCore.QueryModel
{
    public class PagingQueryModelContract
    {
        public PagingQueryModelContract(int pageSize, int pageNumber, string sort, string order)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Sort = sort;
            Order = order;
        }
        public PagingQueryModelContract(int pageSize, int pageNumber, string sort, string order, List<SearchParameter> searchParameters)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Sort = sort;
            Order = order;
            SearchParameters = searchParameters;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public List<SearchParameter> SearchParameters { get; set; }
    }

    public class PagingContract
    {
        public PagingContract()
        {
        }

        public PagingContract(int pageSize, int pageNumber, string sort, string order)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Sort = sort;
            Order = order;
        }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }

    }

    public class SearchParameter
    {
        public string Filed { get; set; }
        public object Value { get; set; }
        public SearchType SearchType { get; set; }
    }

    public enum SearchType
    {
        ContainText = 1,
        NotEqualValue = 2,
        EqualValue = 3,
        SearchIdInArray = 4
    }
}