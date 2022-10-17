using Microsoft.AspNetCore.Http;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.File
{
    public class FileDto
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }

        public FileType Type { get; set; }
    }
}
