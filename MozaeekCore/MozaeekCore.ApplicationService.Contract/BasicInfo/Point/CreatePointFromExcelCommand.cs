using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.BasicInfo.Point
{
    public class CreatePointFromExcelCommand: Command
    {
        public string ExcelPath { get; set; }
    }
}
