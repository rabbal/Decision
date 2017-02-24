using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Validators
{
    public sealed class MaximumFileSizeValidator
        : ValidationAttribute, IClientValidatable
    {
        private readonly string _errorMessage = "{0} can not be larger than {1} MB";

        /// <param name="maximumFileSize">Maximum file size in MB</param>
        public MaximumFileSizeValidator(
            double maximumFileSize)
        {
            MaximumFileSize = maximumFileSize;
        }

        /// <summary>
        ///     Maximum file size in MB
        /// </summary>
        public double MaximumFileSize { get; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            System.Web.Mvc.ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "maximumfilesize"
            };

            clientValidationRule.ValidationParameters.Add("size", MaximumFileSize);

            return new[] {clientValidationRule};
        }

        public override bool IsValid(
            object value)
        {
            if (value == null)
            {
                return true;
            }

            if (IsValidMaximumFileSize((value as HttpPostedFileBase).ContentLength)) return true;
            ErrorMessage = string.Format(_errorMessage, "{0}", MaximumFileSize);
            return false;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return string.Format(_errorMessage, name, MaximumFileSize);
        }

        private bool IsValidMaximumFileSize(
            int fileSize)
        {
            return ConvertBytesToMegabytes(fileSize) <= MaximumFileSize;
        }

        private static double ConvertBytesToMegabytes(
            int bytes)
        {
            return bytes/1024f/1024f;
        }
    }
}