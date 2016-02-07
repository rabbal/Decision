using System;
using System.Collections.Generic;
using System.ComponentModel;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.AnswerOption;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Question
{
    /// <summary>
    /// ویومدل نمایش سوال
    /// </summary>
    public class QuestionViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی سوال
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// عنوان سوال
        /// </summary>
        [DisplayName("عنوان")]
        public string Title { get; set; }
        /// <summary>
        /// وزن ارزشی سوال
        /// </summary>
        [DisplayName("وزن ارزش سوال")]
        public byte Weight { get; set; }

        /// <summary>
        /// گزینه های سوال چند گزینه ای
        /// </summary>
        public  IList<AnswerOptionViewModel> Options { get; set; }
        #endregion 

    }
}
