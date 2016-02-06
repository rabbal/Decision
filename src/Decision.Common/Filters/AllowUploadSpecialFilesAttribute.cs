using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decision.Utility.MimeTypeHelpers;

namespace Decision.Common.Filters
{
    public class AllowUploadSpecialFilesOnlyAttribute : ActionFilterAttribute
    {

        #region Fields
        private readonly string _extensionsWhiteList;
        private readonly List<string> _toFilter = new List<string>();
        private readonly bool _justImage;
        #endregion

        #region Constcutor
        public AllowUploadSpecialFilesOnlyAttribute(string extensionsWhiteList, bool justImage)
        {
            if (string.IsNullOrWhiteSpace(extensionsWhiteList))
                throw new ArgumentNullException(nameof(extensionsWhiteList));
            _justImage = justImage;
            _extensionsWhiteList = extensionsWhiteList;
            var extensions = extensionsWhiteList.Split(',');
            foreach (var ext in extensions.Where(ext => !string.IsNullOrWhiteSpace(ext)))
            {
                _toFilter.Add(ext.ToLowerInvariant().Trim());
            }
        }
        #endregion

        #region CanUpload
        private bool CanUpload(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;

            var extention = Path.GetExtension(fileName.ToLowerInvariant());
            return _toFilter.Contains(extention);
        }

        #endregion

        #region OnActionExecuting Override
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var files = filterContext.HttpContext.Request.Files;
            foreach (var postedFile in files.Cast<string>().Select(file => files[file]).Where(postedFile => postedFile != null && postedFile.ContentLength != 0))
            {
                if (_justImage)
                    if (!IsImageFile(postedFile)) return;

                if (!CanUpload(postedFile.FileName))
                    throw new InvalidOperationException(
                        $"You are not allowed to upload {Path.GetFileName(postedFile.FileName)} file. Please upload only these files: {_extensionsWhiteList}.");
             
            }

            base.OnActionExecuting(filterContext);
        }
        #endregion

        #region IsImageFile
        public static bool IsImageFile(HttpPostedFileBase photoFile)
        {
            using (var img = Image.FromStream(photoFile.InputStream))
            {
                return img.Width > 0;
            }
        }
        #endregion

    }
}