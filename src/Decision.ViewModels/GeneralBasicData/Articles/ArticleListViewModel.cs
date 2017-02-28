using System.Collections.Generic;

namespace Decision.ViewModels.GeneralBasicData.Articles
{
    public class ArticleListViewModel
    {
        public ArticleSearchRequest SearchRequest { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}