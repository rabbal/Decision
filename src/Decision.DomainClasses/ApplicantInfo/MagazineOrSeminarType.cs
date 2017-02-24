using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum MagazineOrSeminarType
    {

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.None))]
        None = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.InternalResearch))]
        InternalResearch,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.InternaNonResearch))]
        InternaNonResearch,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ExternalIndexed))]
        ExternalIndexed,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ExternalNotIndexed))]
        ExternalNotIndexed
    }
}