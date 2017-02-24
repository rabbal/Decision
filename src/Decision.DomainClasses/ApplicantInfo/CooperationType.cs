using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum CooperationType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.None))]
        None = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Accountable))]
        Accountable ,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CoWorker))]
        Coworker
    }
}