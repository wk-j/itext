using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FreeText {
    class Program {
        static void Main(string[] args) {
            ManipulatePdf();
        }
        static void ManipulatePdf() {

            using (var ms = new MemoryStream()) {

                var pdf = "resources/Project.pdf";
                var reader = new iTextSharp.text.pdf.PdfReader(pdf);
                var stamper = new iTextSharp.text.pdf.PdfStamper(reader, ms);

                var box = reader.GetCropBox(1);
                var left = box.Left;
                var top = box.Top;

                var newRectangle = new iTextSharp.text.Rectangle(left + 20, top - 20, left + 250, top - 40);
                var pcb = new iTextSharp.text.pdf.PdfContentByte(stamper.Writer);
                pcb.SetColorFill(iTextSharp.text.BaseColor.RED);

                var annot = iTextSharp.text.pdf.PdfAnnotation.CreateFreeText(stamper.Writer, newRectangle, "Hello, world!", pcb);
                annot.Flags = iTextSharp.text.pdf.PdfAnnotation.FLAGS_PRINT;

                annot.BorderStyle = new iTextSharp.text.pdf.PdfBorderDictionary(0, 0);
                stamper.AddAnnotation(annot, 1);

                var output = ms.ToArray();
                System.IO.File.WriteAllBytes(".output/Hello.pdf", output);

            }
        }
    }
}
