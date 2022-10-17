using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekTechnicianProfile.Domain
{
    public class TechnicianAttachement
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public long CoreFileId { get; set; }
        public FileType Type { get; set; }

    }
}
