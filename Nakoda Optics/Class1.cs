using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using PdfSharp.Pdf.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Threading.Tasks;

namespace Nakoda_Optics
{
    internal class Class1
    {


        public void Manmera()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            //XFont font = new XFont("Ariel",20,XFontStyle.Bold, options);
           XFont font = new XFont(System.Drawing.FontFamily.GenericSansSerif ,20, XFontStyle.Bold);
              //XFont font = new XFont(System.Drawing.Font );

           

            

            gfx.DrawString("hello", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormat.Center);

            string filename = "hello.pdf";
            document.Save(filename);

        }

    }
}
