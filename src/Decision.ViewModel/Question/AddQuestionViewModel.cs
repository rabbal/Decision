using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.AnswerOption;

namespace Decision.ViewModel.Question
{
    /// <summary>
    /// ویومدل درج سوال
    /// </summary>
    public class AddQuestionViewModel
    {
        #region Properties
        /// <summary>
        /// عنوان سوال
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [DisplayName("عنوان")]
        public  string Title { get; set; }

        /// <summary>
        /// وزن ارزشی سوال
        /// </summary>
        [DisplayName("وزن ارزش سوال")]
        [Required(ErrorMessage = "لطفا ورزن ارزشی سوال را مشخص کنید")]
        public  byte Weight { get; set; }

        /// <summary>
        /// گزینه های سوال چند گزینه ای
        /// </summary>
        public  IList<AddAnswerOptionViewModel> Options { get; set; }
        #endregion
    }
}