using SautinSoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPdfJpg
{
    public class Excel2Pdf
    {
        public static string Convert(string excelFileName)
        {
            ExcelToPdf x = new ExcelToPdf();
            string pdfFileName = Path.ChangeExtension(excelFileName, ".pdf"); ;

            x.OutputFormat = SautinSoft.ExcelToPdf.eOutputFormat.Pdf;
            try
            {
                x.ConvertFile(excelFileName, pdfFileName);
                //System.Diagnostics.Process.Start(pdfFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return pdfFileName;
        }
    }
}
