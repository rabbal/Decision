using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum MagazineOrSeminarType
    {
        [Display(Name = "داخلی علمی پژوهشی")] InternalResearch,

        [Display(Name = "داخلی غیر علمی پژوهشی")] InternaNonResearch,

        [Display(Name = "خارجی ایندکس شده")] ExternalIndexed,

        [Display(Name = "خارجی ایندکس شده")] ExternalNotIndexed
    }
}