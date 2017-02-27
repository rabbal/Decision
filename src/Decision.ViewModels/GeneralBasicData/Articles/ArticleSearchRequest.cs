using System;

namespace Decision.ViewModels.GeneralBasicData.Article
{
    public class ArticleSearchRequest : ListRequestBase
    {
         public Guid ApplicantId { get; set; }
    }
}