using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.File
{   
    public class CreateFileCommandResult
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
