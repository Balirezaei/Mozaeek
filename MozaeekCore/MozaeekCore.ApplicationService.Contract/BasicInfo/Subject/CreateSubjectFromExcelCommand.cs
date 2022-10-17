using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateSubjectFromExcelCommand : Command
    {
        public string ExcelPath { get; set; }
    }
}