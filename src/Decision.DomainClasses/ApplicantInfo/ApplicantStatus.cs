using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum ApplicantStatus
    {
        [Display(Name = "معلق")] Pending,

        [Display(Name = "بررسی اولیه")] InitialReview,

        [Display(Name = "بررسی هیئت جذب")] InProgress,

        [Display(Name = "تأیید شده توسط هیئت جذب")] Approved,

        [Display(Name = "پذیرفته شده")] Accepted
    }
}