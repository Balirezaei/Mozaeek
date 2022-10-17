using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.QueryModel
{
    public class TechnicianAttachmentQuery: BaseQuery
    {
        public long FileId { get; set; }
        public string FileName { get; set; }
        public string FileHttpAddress { get; set; }
    }
}
