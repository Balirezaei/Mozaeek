﻿using MozaeekTechnicianProfile.Core.Core.Base;

namespace MozaeekTechnicianProfile.ApplicationService.Contract
{
    public class PagingContract : Query
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }

    }
}