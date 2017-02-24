using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    
    public enum  MilitaryStatus
    {
        
        [Display(Name = "خدمت کرده")]
        Served,
        
        [Display(Name = "معافیت تحصیلی")]
        EducationPardon,
        
        [Display(Name = "معاف دائم")]
        PermanentExemption,
        
        [Display(Name ="مشغول خدمت")]
        Serving


    }
}
