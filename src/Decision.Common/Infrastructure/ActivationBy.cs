using System.ComponentModel.DataAnnotations;

namespace NTierMvcFramework.Common.Infrastructure
{
    public enum ActivationBy
    {
        [Display(Name = "ایمیل")]
        Email,
        [Display(Name = "پیامک")]
        Sms
    }
}