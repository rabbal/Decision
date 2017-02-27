using System.ComponentModel.DataAnnotations;

namespace Decision.Framework.Domain.Models
{
    public enum PageSize
    {
        [Display(Name = "همه")]
        All = 0,
        [Display(Name = "۱۰")]
        Count10 = 10,
        [Display(Name = "۲۰")]
        Count20 = 20,
        [Display(Name = "۳۰")]
        Count30 = 30,
        [Display(Name = "۴۰")]
        Count40 = 40,
        [Display(Name = "۵۰")]
        Count50 = 50,
        [Display(Name = "۱۰۰")]
        Count100 = 100,
    }
}
