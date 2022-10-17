using MozaeekCore.Common.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ViewModel
{
    public class FullTextSearchDto
    {
        public int AnnouncementPageNumber { get; set; }
        public int RequestTargetPageNumber { get; set; }
        public string Query { get; set; }
    }

    //public class FullUserSearchByLabel
    //{
    //    public long LabelId { get; set; }
    //}

    public class FullUserSearchByRequestOrg
    {
        public long RequestOrgId { get; set; }
    }

    public class FullUserSearchBySubject
    {
        public long SubjectId { get; set; }
    }

    public class FullUserSearchByUserCharacteristics
    {
        public List<long> LabelIds { get; set; }
    }
    public class FullUserSearchByRequestTarget
    {
        public long RequestTargetId { get; set; }
    }
}
