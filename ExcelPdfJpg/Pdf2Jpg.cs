using PdfPrintingNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPdfJpg
{
    public class Pdf2Jpg
    {
        public static string Convert(string pdfFileName)
        {
            PdfPrint p = new PdfPrint(pdfFileName, "");
            string jpgFileName = Path.ChangeExtension(pdfFileName, ".jpg"); ;
            p.SavePdfPagesAsImages(pdfFileName, jpgFileName);
            string[] jpgFiles = Directory.GetFiles(Path.GetDirectoryName(jpgFileName), "*.jpg");
            if (jpgFiles != null && jpgFiles.Length > 0)
                return jpgFiles[0];
            else
                return null;
        }
    }
}
