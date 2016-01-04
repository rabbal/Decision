using System.Web.Mvc;

namespace Decision.ViewModel.ArticleEvaluation
{
   public class StringQuestion:BaseQuestion
    {
        [AllowHtml]
        public string Value { get; set; }
    }
}
