using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ScannerService
{
    public class ImageToPdf
    {
        public Rectangle PdfPageSize { set; get; }
        public ImageFormat ImageCompressionFormat { set; get; }
        public bool FitImagesToPage { set; get; }

        public string ExportToPdfAsBase64String(IEnumerable<System.Drawing.Image> imageFiles)
        {
            var result = "";
            using (var pdfDoc = new Document(PdfPageSize))
            {
                using (var stream = new MemoryStream())
                {
                    var pdfWriter = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfWriter.SetPdfVersion(new PdfName("1.5"));
                    pdfWriter.CompressionLevel = PdfStream.BEST_COMPRESSION;

                    pdfDoc.Open();

                    foreach (var img in imageFiles.Select(bmpImage => Image.GetInstance(bmpImage, ImageFormat.Bmp)))
                    {
                        if (FitImagesToPage)
                        {
                            img.ScaleAbsolute(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                        }
                        img.SetAbsolutePosition(0, 0);

                        pdfDoc.Add(img);
                        pdfDoc.NewPage();
                    }

                    result = Convert.ToBase64String(stream.ToArray());
                }

            }
            return result;
        }
    }
}
