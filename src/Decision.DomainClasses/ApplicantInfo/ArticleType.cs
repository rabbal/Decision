using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{

    public enum ArticleType
    {

        [Display(Name = "کامل اصیل پژوهشی")]
        OriginalArticle,
       
        [Display(Name = "مروری")]
        ReviewArticle,
       
        [Display(Name = "مقاله کوتاه")]
        ShortCommunication,
        
        [Display(Name = "Case Series")]
        CaseSeries,
        
        [Display(Name = "گزارش موارد نادر")]
        CaseReport,
        
        [Display(Name = "Research Letter")]
        ResearchLetter,
     
        [Display(Name = "Letter To Editor")]
        LetterToEditor,
        
        [Display(Name = "غیره")]
        Other

    }
}
