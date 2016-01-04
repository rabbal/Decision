using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.Controller
{
    public static class FileManager
    {
        #region Fields
        private const string _imagesFolderPath = "~/File/image";
        private const string _avatarsFolderPath = "~/File/avatar";
        private const string _userFileFolderPath = "~/File/userFile";
        #endregion

        public static string UploadFile(this BaseController controller, HttpPostedFileBase postedFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
            var imagePath = Path.Combine(controller.Server.MapPath(_avatarsFolderPath), fileName);
            postedFile.SaveAs(imagePath);
            return fileName;
        }
    }
}
