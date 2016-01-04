using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModel.AnswerOption;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Question
{
    /// <summary>
    /// ویومدل ویرایش سوال
    /// </summary>
    public class EditQuestionViewModel : BaseIsDelete
    {
        #region Properties
        /// <summary>
        /// آی دی سوال
        /// </summary>
        public  Guid Id { get; set; }
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
        public  byte Weight { get; set; }

        /// <summary>
        /// گزینه های سوال چند گزینه ای
        /// </summary>
        public  IList<EditAnswerOptionViewModel> Options { get; set; }
        #endregion 
    }
}