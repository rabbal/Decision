using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Validators
{
    public sealed class MinimumFileSizeValidator
        : ValidationAttribute, IClientValidatable
    {        
        private string _errorMessage = "{0} can not be smaller than {1} MB";     

        /// <summary>
        /// Minimum file size in MB
        /// </summary>
        public double MinimumFileSize { get; private set; }

        /// <param name="minimumFileSize">MinimumFileSize file size in MB</param>
        public MinimumFileSizeValidator(
           double minimumFileSize)
        {
            MinimumFileSize = minimumFileSize;
        }

        public override bool IsValid(
             object value)
        {
            var httpPostedFileBase
                 = value as HttpPostedFileBase;
            if (httpPostedFileBase != null && IsValidMinimumFileSize(httpPostedFileBase.ContentLength)) return true;
            ErrorMessage = string.Format(_errorMessage, "{0}", MinimumFileSize);
            return false;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return string.Format(_errorMessage, name, MinimumFileSize);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
              System.Web.Mvc.ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage   = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "minimumfilesize"
            };

            clientValidationRule.ValidationParameters.Add("size", MinimumFileSize);

            return new[] { clientValidationRule };
        }

        private bool IsValidMinimumFileSize(
            int fileSize)
        {
            return ConvertBytesToMegabytes(fileSize) >= MinimumFileSize;
        }

        private static double ConvertBytesToMegabytes(
            int bytes)
        {
            return (bytes / 1024f) / 1024f;
        }        
    }
}