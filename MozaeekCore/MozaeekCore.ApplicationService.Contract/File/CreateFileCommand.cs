using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract.File
{
    public class CreateFileCommand:Command
    {
        public IFormFile File { get; set; }
        public FileType Type { get; set; }
    }
}
