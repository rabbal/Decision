using System;
using System.ComponentModel;

namespace Decision.ViewModel.AnswerOption
{
    /// <summary>
    /// ویومدل نمایش گزینه ی سوال چند گزینه ای
    /// </summary>
    public class AnswerOptionViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی گزینه
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// نام گزینه پاسخ
        /// </summary>
        [DisplayName("متن گزینه")]
        public string Name { get; set; }
        /// <summary>
        /// وزن ارزشی گزینه
        /// </summary>
        [DisplayName("وزن ارزشی گزینه")]
        public byte Weight { get; set; }
        #endregion
    }
}