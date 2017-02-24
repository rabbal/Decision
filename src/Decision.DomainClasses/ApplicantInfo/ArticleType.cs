using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده نوع مقاله
    /// </summary>
    public enum ArticleType
    {
        /// <summary>
        /// کامل اصیل پژوهشی
        /// </summary>
        [Display(Name = "کامل اصیل پژوهشی")]
        OriginalArticle,
        /// <summary>
        /// مروری
        /// </summary>
        [Display(Name = "مروری")]
        ReviewArticle,
        /// <summary>
        /// مقاله کوتاه
        /// </summary>
        [Display(Name = "مقاله کوتاه")]
        ShortCommunication,
        /// <summary>
        /// Case Series
        /// </summary>
        [Display(Name = "Case Series")]
        CaseSeries,
        /// <summary>
        /// گزارش موارد نادر
        /// </summary>
        [Display(Name = "گزارش موارد نادر")]
        CaseReport,
        /// <summary>
        /// Research Letter
        /// </summary>
        [Display(Name = "Research Letter")]
        ResearchLetter,
        /// <summary>
        /// Letter To Editor
        /// </summary>
        [Display(Name = "Letter To Editor")]
        LetterToEditor,
        /// <summary>
        /// غیره
        /// </summary>
        [Display(Name = "غیره")]
        Other

    }
}
