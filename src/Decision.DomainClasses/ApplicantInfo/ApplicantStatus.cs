using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum ApplicantStatus
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Pending))]
        Pending = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.InitialReview))]
        InitialReview,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.InProgress))]
        InProgress,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Approved))]
        Approved,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Accepted))]
        Accepted
    }
}