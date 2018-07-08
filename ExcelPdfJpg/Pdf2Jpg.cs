using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfiumViewer;
using System.Drawing.Imaging;

namespace ExcelPdfJpg
{
    public class Pdf2Jpg
    {
        public static string Convert(string pdfFileName)
        {
            try
            {
                using (var document = PdfDocument.Load(pdfFileName))
                {
                    var image = document.Render(0, 300, 300, true);//300
                    var img = Path.ChangeExtension(pdfFileName, "png");
                    image.Save(img, ImageFormat.Png);
                    return img;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
