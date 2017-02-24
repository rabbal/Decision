using System.Collections.Generic;

namespace Decision.ViewModel.Question
{
    /// <summary>
    /// ویو مدل نمایش لیست سوال ها
    /// </summary>
    public class QuestionListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public QuestionSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش سوال
        /// </summary>
        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}