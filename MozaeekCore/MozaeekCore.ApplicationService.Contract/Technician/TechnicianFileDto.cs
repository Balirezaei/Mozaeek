using Microsoft.AspNetCore.Http;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Technician
{
   public class TechnicianFileDto
    {
        public IFormFile File { get; set; }
        public FileType FileType { get; set; }
        public string Title { get; set; }
    }
}
