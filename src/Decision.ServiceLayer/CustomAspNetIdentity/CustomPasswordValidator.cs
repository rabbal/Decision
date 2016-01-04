using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.CustomAspNetIdentity
{

    public class CustomPasswordValidator : IIdentityValidator<string>
    {
        #region Properties
        public int RequiredLength { get; set; }
        public bool RequireNonLetterOrDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
        #endregion

        #region IIdentityValidator
        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var list = new List<string>();

            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
                list.Add(string.Format("کلمه عبور نباید کمتر از 6 کاراکتر باشد"));

            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
                list.Add("برای امنیت بیشتر از حداقل از یک کارکتر غیر عددی و غیر حرف  برای کلمه عبور استفاده کنید");

            if (RequireDigit && item.All(c => !IsDigit(c)))
                list.Add("برای امنیت بیشتر از اعداد هم در کلمه عبور استفاده کنید");
            if (RequireLowercase && item.All(c => !IsLower(c)))
                list.Add("از حروف کوچک نیز برای کلمه عبور استفاده کنید");
            if (RequireUppercase && item.All(c => !IsUpper(c)))
                list.Add("از حروف بزرک نیز برای کلمه عبور استفاده کنید");
            return Task.FromResult(list.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(string.Join(" ", list)));
        }

        #endregion

        #region PrivateMethods
        public virtual bool IsDigit(char c)
        {
            if (c >= 48)
                return c <= 57;
            return false;
        }

        public virtual bool IsLower(char c)
        {
            if (c >= 97)
                return c <= 122;
            return false;
        }


        public virtual bool IsUpper(char c)
        {
            if (c >= 65)
                return c <= 90;
            return false;
        }

        public virtual bool IsLetterOrDigit(char c)
        {
            if (!IsUpper(c) && !IsLower(c))
                return IsDigit(c);
            return true;
        }
        #endregion

    }
}