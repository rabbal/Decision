using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده نوع مقاله
    /// </summary>
    public enum ArticleType
    {
        OriginalArticle,
        ReviewArticle,
        ShortCommunication,
        CaseSeries,
        CaseReport,
        ResearchLetter,
        LetterToEditor,
        Other

    }
}
