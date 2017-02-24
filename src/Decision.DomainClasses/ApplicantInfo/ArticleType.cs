using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum ArticleType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.None))]
        None = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.OriginalArticle))]
        OriginalArticle,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ReviewArticle))]
        ReviewArticle,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ShortCommunication))]
        ShortCommunication,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CaseSeries))]
        CaseSeries,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CaseReport))]
        CaseReport,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ResearchLetter))]
        ResearchLetter,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.LetterToEditor))]
        LetterToEditor
    }
}