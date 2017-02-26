//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Decision.Framework.Utility;
//using Microsoft.AspNet.Identity;

//namespace Decision.Framework.IdentityToolkit.Validators
//{
//    public sealed class PasswordValidator : IIdentityValidator<string>
//    {
//        private readonly bool _preventValidation;

//        public PasswordValidator()
//        {
//            _preventValidation = true;
//        }

//        #region IIdentityValidator Members

//        public Task<IdentityResult> ValidateAsync(string item)
//        {
//            if (_preventValidation) return Task.FromResult(IdentityResult.Success);

//            if (item == null)
//                throw new ArgumentNullException(nameof(item));
//            var errors = new List<string>();

//            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
//                errors.Add("کلمه عبور نباید کمتر از 6 کاراکتر باشد");

//            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
//                errors.Add("برای امنیت بیشتر از حداقل از یک کارکتر غیر عددی و غیر حرف  برای کلمه عبور استفاده کنید");

//            if (RequireDigit && item.All(c => !IsDigit(c)))
//                errors.Add("برای امنیت بیشتر از اعداد هم در کلمه عبور استفاده کنید");

//            if (RequireLowercase && item.All(c => !IsLower(c)))
//                errors.Add("از حروف کوچک نیز برای کلمه عبور استفاده کنید");

//            if (RequireUppercase && item.All(c => !IsUpper(c)))
//                errors.Add("از حروف بزرگ نیز برای کلمه عبور استفاده کنید");

//            if (!item.IsSafePasword())
//            {
//                errors.Add("کله عبور وارد شده به راحتی قابل تشخیص میباشد");
//            }
//            return
//                Task.FromResult(errors.Count == 0
//                    ? IdentityResult.Success
//                    : IdentityResult.Failed(string.Join(" ", errors)));
//        }

//        #endregion

//        #region Properties

//        public int RequiredLength { get; set; }
//        public bool RequireNonLetterOrDigit { get; set; }
//        public bool RequireLowercase { get; set; }
//        public bool RequireUppercase { get; set; }
//        public bool RequireDigit { get; set; }

//        #endregion

//        #region PrivateMethods

//        public bool IsDigit(char c)
//        {
//            if (c >= 48)
//                return c <= 57;
//            return false;
//        }

//        public bool IsLower(char c)
//        {
//            if (c >= 97)
//                return c <= 122;
//            return false;
//        }


//        public bool IsUpper(char c)
//        {
//            if (c >= 65)
//                return c <= 90;
//            return false;
//        }

//        public bool IsLetterOrDigit(char c)
//        {
//            if (!IsUpper(c) && !IsLower(c))
//                return IsDigit(c);
//            return true;
//        }

//        #endregion
//    }
//}