using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.GeneralBasicData.Articles
{
    public class ArticleSearchRequest : ListRequestBase
    {
         public Guid ApplicantId { get; set; }
    }
}