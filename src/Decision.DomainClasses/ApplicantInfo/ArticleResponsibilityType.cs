using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum ArticleResponsibilityType
    {
        [Display(Name = "نویسنده مسئول")] CorrespondingAuthor,

        [Display(Name = "نفر اول")] FirstOne,

        [Display(Name = "همکار")] CoWorker,

        [Display(Name = "دانشجو")] Student,

        [Display(Name = "هیئت علمی")] ScienceCommittee,

        [Display(Name = "غیر هیئت علمی")] NonScienceCommittee
    }
}