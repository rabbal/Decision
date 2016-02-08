using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Decision.Utility;

namespace Decision.Common.Helpers
{
    public static class FieldValidationHelper
    {
        private static readonly Regex AllDigitEqual = new Regex(@"(\d)\1{9}", RegexOptions.Compiled);
        private static readonly Regex NumberOnlyRegex = new Regex(@"\d{10}", RegexOptions.Compiled);

        #region NationalCode
        /// <summary>
        /// تعیین معتبر بودن کد ملی
        /// </summary>
        /// <param name="nationalCode">کد ملی وارد شده</param>
        /// <returns>
        /// در صورتی که کد ملی صحیح باشد خروجی <c>true</c> و در صورتی که کد ملی اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static bool IsValidNationalCode(this string nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد
            nationalCode = nationalCode.GetEnglishNumber();
            if (string.IsNullOrEmpty(nationalCode))
                throw new Exception("لطفا کد ملی را صحیح وارد نمایید");
            
            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            if (!NumberOnlyRegex.IsMatch(nationalCode))
                throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            if (AllDigitEqual.IsMatch(nationalCode))
                return false;

            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString(CultureInfo.InvariantCulture)) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString(CultureInfo.InvariantCulture)) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString(CultureInfo.InvariantCulture)) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString(CultureInfo.InvariantCulture)) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString(CultureInfo.InvariantCulture)) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString(CultureInfo.InvariantCulture)) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString(CultureInfo.InvariantCulture)) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString(CultureInfo.InvariantCulture)) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString(CultureInfo.InvariantCulture)) * 2;
            var a = Convert.ToInt32(chArray[9].ToString(CultureInfo.InvariantCulture));

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        #endregion

    }
}
