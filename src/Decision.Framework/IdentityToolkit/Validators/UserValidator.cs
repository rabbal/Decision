//using System;
//using System.Collections.Generic;
//using System.Net.Mail;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;

//namespace Decision.Framework.IdentityToolkit.Validators
//{
//    public sealed class UserValidator<TUser, TKey> : IIdentityValidator<User>
//        where TUser : class, IUser<long>
//        where TKey : IEquatable<long>
//    {
//        private readonly bool PreventValidation;
//        private readonly List<string> _allowedEmailDomains =
//            new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };
        
//        public bool AllowOnlyAlphanumericUserNames { get; set; }
//        public bool RequireUniqueEmail { get; set; }

//        private UserService UserService { get; set; }
//        public CustomUserValidator(UserService userService)
//        {
//            if (userService == null)
//                throw new ArgumentNullException(nameof(userService));
//            PreventValidation = true;
//            AllowOnlyAlphanumericUserNames = true;
//            UserService = userService;
//        }
//        public async Task<IdentityResult> ValidateAsync(User item)
//        {
//            if(PreventValidation) return IdentityResult.Success;

//            if (item == null)
//                throw new ArgumentNullException(nameof(item));
//            var errors = new List<string>();
//            await  ValidateUserName(item, errors).ConfigureAwait(false);
//            if (RequireUniqueEmail)
//                await  ValidateEmailAsync(item, errors).ConfigureAwait(false);
//            return errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
//        }

//        private async Task ValidateUserName(User user, ICollection<string> errors)
//        {
//            if (string.IsNullOrWhiteSpace(user.UserName))
//                errors.Add(" نام کاربری خود را وارد کنید");
//            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
//            {
//                errors.Add(" برای نام کاربری فقط از کاراکتر های مجاز استفاده کنید");
//            }
//            else
//            {
//                var owner = await  UserService.FindByNameAsync(user.UserName).ConfigureAwait(false);
//                if (owner != null && !EqualityComparer<long>.Default.Equals(owner.Id, user.Id))
//                    errors.Add("نام کاربری مورد نظر قبلا در سیستم ثبت شده است");
//            }
//        }

//        private async Task ValidateEmailAsync(User user, ICollection<string> errors)
//        {
//            var email = await UserService.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
//            if (string.IsNullOrWhiteSpace(email))
//            {
//                errors.Add(" آدرس ایمیل خود را وارد کنید");
//            }
//            else
//            {
//                try
//                {
//                    var m = new MailAddress(email);
//                }
//                catch (FormatException)
//                {
//                    errors.Add(" آدرس ایمیل را به شکل صحیح وارد کنید");
//                    return;
//                }

//                var emailDomain = user.Email.Split('@')[1];
//                if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
//                {
//                    errors.Add("دامنه مرتبط با ایمیل وارد شده، غیر مجار است.");
//                }

//                var owner = await  UserService.FindByEmailAsync(email).ConfigureAwait(false);
//                if (owner != null && !EqualityComparer<long>.Default.Equals(owner.Id, user.Id))
//                    errors.Add("آدرس ایمیل مورد نظر قبلا در سیستم ثبت شده است");
//            }

           
//        }
//    }

//}
