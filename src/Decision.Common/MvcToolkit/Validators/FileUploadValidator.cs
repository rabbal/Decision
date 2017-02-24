using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Decision.Common.Extensions;

namespace Decision.Common.MvcToolkit.Validators
{
    public sealed class FileUploadValidator
        : ValidationAttribute, IClientValidatable
    {
        private readonly MaximumFileSizeValidator _maximumFileSizeValidator;
        private readonly MinimumFileSizeValidator _minimumFileSizeValidator;
        private readonly ValidFileTypeValidator _validFileTypeValidator;

        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(
            params string[] validFileTypes)
        {
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }

        /// <param name="maximumFileSize">Maximum file size in MB</param>
        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(
            double maximumFileSize
            , params string[] validFileTypes)
        {
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }

        /// <param name="minimumFileSize">MinimumFileSize file size in MB</param>
        /// <param name="maximumFileSize">Maximum file size in MB</param>
        /// <param name="validFileTypes">Valid file extentions(without the dot)</param>
        public FileUploadValidator(
            double minimumFileSize
            , double maximumFileSize
            , params string[] validFileTypes)
        {
            _minimumFileSizeValidator = new MinimumFileSizeValidator(minimumFileSize);
            _maximumFileSizeValidator = new MaximumFileSizeValidator(maximumFileSize);
            _validFileTypeValidator = new ValidFileTypeValidator(validFileTypes);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            System.Web.Mvc.ModelMetadata metadata
            , ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "fileuploadvalidator"
            };

            var clientvalidationmethods = new List<string>();
            var parameters = new List<string>();
            var errorMessages = new List<string>();

            if (_minimumFileSizeValidator != null)
            {
                clientvalidationmethods.Add(
                    _minimumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_minimumFileSizeValidator.MinimumFileSize.ToString(CultureInfo.InvariantCulture));
                errorMessages.Add(_minimumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_maximumFileSizeValidator != null)
            {
                clientvalidationmethods.Add(
                    _maximumFileSizeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(_maximumFileSizeValidator.MaximumFileSize.ToString(CultureInfo.InvariantCulture));
                errorMessages.Add(_maximumFileSizeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            if (_validFileTypeValidator != null)
            {
                clientvalidationmethods.Add(
                    _validFileTypeValidator.GetClientValidationRules(metadata, context).First().ValidationType);
                parameters.Add(string.Join(",", _validFileTypeValidator.ValidFileTypes));
                errorMessages.Add(_validFileTypeValidator.FormatErrorMessage(metadata.GetDisplayName()));
            }

            clientValidationRule.ValidationParameters.Add(nameof(clientvalidationmethods),
                clientvalidationmethods.ToConcatenatedString(","));
            clientValidationRule.ValidationParameters.Add(nameof(parameters), parameters.ToConcatenatedString("|"));
            clientValidationRule.ValidationParameters.Add("errormessages", errorMessages.ToConcatenatedString(","));

            yield return clientValidationRule;
        }

        protected override ValidationResult IsValid(
            object value
            , ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            try
            {
                if (value.GetType() != typeof(HttpPostedFileWrapper))
                {
                    throw new InvalidOperationException("");
                }

                var errorMessage = new StringBuilder();
                var file = value as HttpPostedFileBase;

                if (_minimumFileSizeValidator != null)
                {
                    if (!_minimumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(
                            $"{_minimumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)}. ");
                    }
                }

                if (_maximumFileSizeValidator != null)
                {
                    if (!_maximumFileSizeValidator.IsValid(file))
                    {
                        errorMessage.Append(
                            $"{_maximumFileSizeValidator.FormatErrorMessage(validationContext.DisplayName)}. ");
                    }
                }

                if (_validFileTypeValidator != null)
                {
                    if (!_validFileTypeValidator.IsValid(file))
                    {
                        errorMessage.Append(
                            $"{_validFileTypeValidator.FormatErrorMessage(validationContext.DisplayName)}. ");
                    }
                }

                return string.IsNullOrEmpty(errorMessage.ToString())
                    ? ValidationResult.Success
                    : new ValidationResult(errorMessage.ToString());
            }
            catch (Exception excp)
            {
                return new ValidationResult(excp.Message);
            }
        }
        
    }
}