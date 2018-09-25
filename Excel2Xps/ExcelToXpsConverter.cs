using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace ExcelPdfJpg
{
    public class ExcelToXpsConverter
    {
        public static string Convert(string excelFileName)
        {
            string xpsFileName = Path.ChangeExtension(excelFileName, ".xps"); ;
            try
            {
                var excelApp = new Excel.Application();
		excelApp.DisplayAlerts = false;
                Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(excelFileName);
                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
                int pagesCount = xlWorksheet.PageSetup.Pages.Count;
                Console.WriteLine(pagesCount);
                xlWorkbook.ExportAsFixedFormat(
                    Excel.XlFixedFormatType.xlTypeXPS,
                    xpsFileName, Excel.XlFixedFormatQuality.xlQualityStandard,
                    true, true,
                    1, pagesCount,
                    false,
                    Type.Missing
                );
                xlWorkbook.Close();
                excelApp.Quit();
                Marshal.ReleaseComObject(xlWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return xpsFileName;
        }
    }
}

