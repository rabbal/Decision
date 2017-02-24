using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    
    public enum MembershipType
    {
        
        [Display(Name = "پیمانی")]
        Contractual,
       
        [Display(Name = "راتبه")]
        Ratbh,
        
        [Display(Name = "طرح سربازی")]
        MilitaryPlan
    }
}
