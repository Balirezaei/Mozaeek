using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MozaeekCore.Common.ExtensionMethod
{
    public static class ImportExcelExt
    {
        public static List<TResult> ImportExcel<TResult>(this string address) where TResult : new()
        {
            var excelengine = new ExcelEngine();
            var xlApp = excelengine.Excel;
            Stream excexlStream = File.Open(address,FileMode.Open);
            var xlWorkBook = xlApp.Workbooks.Open(excexlStream);//xlApp.Workbooks.Open(address, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorkSheet = xlWorkBook.Worksheets[0];// (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            var result = new List<TResult>();
            var props = typeof(TResult).GetProperties().ToList();
            var colRange = (xlWorkSheet.UsedRange.Columns[1]);
            var lastRow = colRange.FindFirst(string.Empty, ExcelFindType.Text).Row;
            for (int i = 1; i < lastRow - 1; i++)
            {
                TResult item = new TResult();
                props.ForEach(p =>
                {
                    var range = xlWorkSheet.UsedRange.Rows[0];
                    var column = range.FindFirst(p.Name,ExcelFindType.Values).Column;
                    var value = Convert.ToString((xlWorkSheet.UsedRange[i+1, column]).Value);
                    var typep = p.PropertyType;
                    p.SetValue(item, value);
                });
                result.Add(item);
            }
            return result;
        }


    }
}
