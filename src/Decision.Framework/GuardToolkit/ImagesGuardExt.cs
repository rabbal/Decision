using System.Drawing;
using System.IO;
using System.Web;

namespace Decision.Framework.GuardToolkit
{
    public static class ImagesGuardExt
    {
        public static bool IsImageFile(this byte[] photoFile)
        {
            if (photoFile == null || photoFile.Length == 0)
                return false;

            using (var memoryStream = new MemoryStream(photoFile))
            {
                using (var img = Image.FromStream(memoryStream))
                {
                    return img.Width > 0;
                }
            }
        }

        public static bool IsValidImageFile(this HttpPostedFileBase photoFile, int maxWidth = 150, int maxHeight = 150)
        {
            if (photoFile == null || photoFile.ContentLength == 0) return false;
            using (var img = Image.FromStream(photoFile.InputStream))
            {
                if (img.Width > maxWidth) return false;
                if (img.Height > maxHeight) return false;
            }
            return true;
        }

        public static bool IsImageFile(this HttpPostedFileBase photoFile)
        {
            if (photoFile == null || photoFile.ContentLength== 0)
                return false;

            using (var img = Image.FromStream(photoFile.InputStream))
            {
                return img.Width > 0;
            }
        }
    }
}