using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Article
{
    public class ArticleSearchRequest : ListRequestBase
    {
         public Guid ApplicantId { get; set; }
    }
}