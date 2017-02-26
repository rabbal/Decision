using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Applicants
{
    public enum ArticleResponsibilityType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.None))]
        None = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CorrespondingAuthor))]
        CorrespondingAuthor ,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.FirstOne))]
        FirstOne,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CoWorker))]
        CoWorker,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Student))]
        Student,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ScienceCommittee))]
        ScienceCommittee,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.NonScienceCommittee))]
        NonScienceCommittee
    }
}