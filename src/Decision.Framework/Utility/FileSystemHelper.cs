using System;
using System.IO;
using System.Web;

namespace Decision.Framework.Utility
{
    public static class FileSystemHelper
    {
       
        public static string GetFileName(this string fileName, string path, string format = "yyyy/MM/dd")
        {
            var root = HttpContext.Current.Server.MapPath("~").TrimEnd('\\');
            var path2 = path.Replace("/", "\\").Trim('\\');
            string dir;
            var time = "";
            if (!string.IsNullOrEmpty(format))
            {
                time = DateTime.Now.ToString(format).Replace("/", "\\").TrimEnd('\\');
                FolderPath(path2 + "\\" + time, true);
                dir = root + "\\" + path2 + "\\" + time;
                //Directory.CreateDirectory(dir);
            }
            else
            {
                FolderPath(path2, true);
                dir = root + "\\" + path2;
            }
            var fileName2 = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var index = 2;
            if (File.Exists(dir + "\\" + fileName2 + ext))
            {
                while (File.Exists(dir + "\\" + fileName2 + string.Format(" ({0})", index) + ext))
                {
                    index++;
                }
                fileName2 += string.Format(" ({0})", index);
            }
            return time + "\\" + fileName2 + ext;
        }

        public static string FolderPath(this string folderNames, bool check)
        {
            var path = HttpContext.Current.Server.MapPath("~").TrimEnd('\\');
            foreach (var folderName in folderNames.Split('\\'))
            {
                path += "\\" + folderName;
                if (check)
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
            }
            return path;
        }
    }
}