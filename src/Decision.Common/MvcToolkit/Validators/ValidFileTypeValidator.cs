using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTierMvcFramework.Common.Extensions;

namespace NTierMvcFramework.Common.MvcToolkit.Validators
{
    public sealed class ValidFileTypeValidator
        : ValidationAttribute, IClientValidatable
    {
        private readonly string _errorMessage = "{0} must be one of the following file types: {1}";

        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public ValidFileTypeValidator(
            params string[] validFileTypes)
        {
            ValidFileTypes = validFileTypes;
        }

        /// <summary>
        ///     Valid file extentions
        /// </summary>
        public string[] ValidFileTypes { get; }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            System.Web.Mvc.ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validfiletype"
            };

            clientValidationRule.ValidationParameters.Add("filetypes", ValidFileTypes.ToConcatenatedString(","));

            return new[] {clientValidationRule};
        }

        public override bool IsValid(
            object value)
        {
            var file = value as HttpPostedFileBase;

            if (value == null || string.IsNullOrEmpty(file.FileName))
            {
                return true;
            }

            if (ValidFileTypes == null) return true;

            var validFileTypeFound = (from validFileType in ValidFileTypes
                let fileNameParts = file.FileName.Split('.')
                where fileNameParts[fileNameParts.Length - 1] == validFileType
                select validFileType).Any();

            if (validFileTypeFound) return true;

            ErrorMessage = string.Format(_errorMessage, "{0}", ValidFileTypes.ToConcatenatedString(","));
            return false;
        }

        public override string FormatErrorMessage(
            string name)
        {
            return string.Format(_errorMessage, name, ValidFileTypes.ToConcatenatedString(","));
        }
    }
}