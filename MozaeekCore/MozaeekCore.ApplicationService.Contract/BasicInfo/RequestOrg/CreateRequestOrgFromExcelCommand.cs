using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateRequestOrgFromExcelCommand : Command
    {
        public string ExcelPath { get; set; }
    }
}