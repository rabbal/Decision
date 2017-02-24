using System.ComponentModel;

namespace Decision.ViewModel.Setting
{
    public class UserSettingsViewModel
    {
        public UserSettingViewModel UserSetting { get; set; }
        public ExternalAutheSettingViewModel ExternalAuthenticationSetting { get; set; }
    }

    public class UserSettingViewModel
    {
        [DisplayName("وضعیت استفاده از نام کاربری")]
        public bool UsernamesEnabled { get; set; }
        [DisplayName("امکان تغییر نام کاربری")]
        public bool AllowUsersToChangeUsername { get; set; }
        [DisplayName("اجازه تغییر آواتار")]
        public bool AllowUsersToUploadAvatars { get; set; }
        [DisplayName("اندازه حجم آواتار")]
        public int AvatarMaximumSizeBytes { get; set; }
        [DisplayName("نمایش آواتار برای پیش فرض")]
        public bool DefaultAvatarEnabled { get; set; }
        [DisplayName("نمایش تاریخ عضویت کاربران")]
        public bool ShowUsersJoinDate { get; set; }
        [DisplayName("امکان مشاهده پروفایل کاربران توسط بقیه")]
        public bool AllowViewingProfiles { get; set; }
        [DisplayName("اعلام کردن عضویت کاربر جدید")]
        public bool NotifyNewUserRegistration { get; set; }
        [DisplayName("امکان عضویت در خبرنامه")]
        public bool NewsletterEnabled { get; set; }
        [DisplayName("نمایش بلوک خبرنامه")]
        public bool HideNewsletterBlock { get; set; }
        [DisplayName("امکان درج تاریخ تولد")]
        public bool DateOfBirthEnabled { get; set; }
    }

    public class ExternalAutheSettingViewModel
    {
        [DisplayName("عضویت اوتوماتیک")]
        public bool AutoRegisterEnabled { get; set; }
        [DisplayName("(ClientId)آی دی کاربری گوگل")]
        public string GoogleClientId { get; set; }
        [DisplayName("(ClientSecret)رمز کاربری گوگل")]
        public string GoogleCientSecret { get; set; }
        [DisplayName("(AppId)آی دی نرم افزار فیسبوک")]
        public string FacebookAppId { get; set; }
        [DisplayName("(AppSecret)رمز کاربری فیسبوک")]
        public string FacebookAppSecret { get; set; }
        [DisplayName("امکان عضویت با اکانت فیسبوک")]
        public bool FacebookSystemEnable { get; set; }
        [DisplayName("امکان عضویت با اکانت گوگل")]
        public bool GoogleSystemEnable { get; set; }
    }
}
