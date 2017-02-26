using System.Collections.Generic;

namespace Decision.ViewModels.Article
{
    public class ArticleListViewModel
    {
        public ArticleSearchRequest SearchRequest { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}