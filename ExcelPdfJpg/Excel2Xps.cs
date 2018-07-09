using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace ExcelPdfJpg
{
    public class Excel2Xps
    {
        public static string Convert(string excelFileName)
        {
             
            string xpsFileName = Path.ChangeExtension(excelFileName, ".xps"); ;

             
            try
            {
                var excelApp = new Excel.Application();
                Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(excelFileName);
                //Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                xlWorkbook.ExportAsFixedFormat(
    Excel.XlFixedFormatType.xlTypeXPS,
    xpsFileName, Excel.XlFixedFormatQuality.xlQualityStandard,
    true, true,
    1, 1,
    false,
    Type.Missing
);

                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return xpsFileName;
        }
    }
}
