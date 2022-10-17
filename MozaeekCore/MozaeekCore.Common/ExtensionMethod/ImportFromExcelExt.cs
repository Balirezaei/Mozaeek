using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace MozaeekCore.Common.ExtensionMethod
{
    public static class ImportFromExcelExt
    {
        public static List<ParentChildExcelDto> ImportExcel(this string address)
        {
            var excelengine = new ExcelEngine();
            var xlApp = excelengine.Excel;
            Stream excexlStream = File.Open(address, FileMode.Open);
            var xlWorkBook = xlApp.Workbooks.Open(excexlStream);//xlApp.Workbooks.Open(address, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorkSheet = xlWorkBook.Worksheets[0];// (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            var result = new List<ParentChildExcelDto>();
            var colRange = (xlWorkSheet.UsedRange.Columns[1]);
            var lastRow = colRange.Count;
            //var lastRow = colRange.FindFirst(string.Empty, ExcelFindType.Text).Row;
            for (int i = 1; i < lastRow; i++)
            {
                if ((xlWorkSheet.UsedRange[i + 1, 1]).Value.IsNullOrEmpty())
                {
                    break;
                }
                ParentChildExcelDto item = new ParentChildExcelDto();
                item.Code = Convert.ToString((xlWorkSheet.UsedRange[i + 1, 1]).Value);
                item.Title = Convert.ToString((xlWorkSheet.UsedRange[i + 1, 2]).Value);
                item.ParentCode = Convert.ToString((xlWorkSheet.UsedRange[i + 1, 3]).Value);

                result.Add(item);
            }
            return result;
        }
    }
}
