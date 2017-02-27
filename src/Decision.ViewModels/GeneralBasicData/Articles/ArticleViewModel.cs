using System;
using System.ComponentModel;

namespace Decision.ViewModels.GeneralBasicData.Article
{
    public class ArticleViewModel :BaseViewModel
    {
        #region Properties
        [DisplayName("کد مقاله")]
        public string Code { get; set; }
        public Guid Id { get; set; }

        [DisplayName("محتوای مقاله")]
        public  string Content { get; set; }

        [DisplayName("خلاصه مقاله")]
        public  string Brief { get; set; }

        [DisplayName("تاریخ ارائه")]
        public  DateTime ArticleDate { get; set; }

        public Guid ApplicantId { get; set; }

        #endregion 
    }
}