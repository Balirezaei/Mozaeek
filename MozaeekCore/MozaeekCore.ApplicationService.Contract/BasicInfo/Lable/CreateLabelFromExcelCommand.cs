using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateLabelFromExcelCommand : Command
    {
        public string ExcelPath { get; set; }
    }
}