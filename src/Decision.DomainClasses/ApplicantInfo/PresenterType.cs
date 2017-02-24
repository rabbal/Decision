using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{

    public enum PresenterType
    {
      
       [Display(Name = "عمومی")]
        General , 
        
        [Display(Name = "علمی")]
        Academic
    }
}
