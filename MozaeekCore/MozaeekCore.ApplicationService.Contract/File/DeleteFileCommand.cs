using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.File
{
    public class DeleteFileCommand:Command
    {
        public DeleteFileCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
