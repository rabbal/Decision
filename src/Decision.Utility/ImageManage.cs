using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace Decision.Utility
{
    public static class ImageManage
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }
        /// <summary>
        /// تغییر اندازه تصویر
        /// </summary>
        /// <returns></returns>
        public static byte[] ResizeImageFile(this byte[] imageFile, int width,int height)
        {
            return new WebImage(imageFile).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }

        public static byte[] ResizeImageFile(string path, int width, int height)
        {
            return new WebImage(path).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }

        public static byte[] ResizeImageFile(this  Stream stream, int width, int height)
        {
            return new WebImage(stream).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }

        public static byte[] AddWaterMark(string filePath, string text)
        {
            using (var img = System.Drawing.Image.FromFile(filePath))
            {
                using (var memStream = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(img)) // to avoid GDI+ errors
                    {
                        bitmap.Save(memStream, ImageFormat.Png);
                        var content = memStream.ToArray();
                        var webImage = new WebImage(memStream);
                        webImage.AddTextWatermark(text, verticalAlign: "Top", horizontalAlign: "Left", fontColor: "Brown");
                        return webImage.GetBytes();
                    }
                }
            }
        }
    }
}