using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.AnswerOption
{
    /// <summary>
    /// ویومدل ویرایش گزینه برای سوال چند گزینه ای
    /// </summary>
    public class EditAnswerOptionViewModel : BaseIsDelete
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
        [Required(ErrorMessage = "لطفا متن پاسخ را وارد کنید")]
        [StringLength(1204, ErrorMessage = "متن گزینه نباید بیشتر از ۱۰۲۴  کاراکتر باشد")]
        
        public  string Name { get; set; }

        /// <summary>
        /// وزن ارزشی گزینه
        /// </summary>
        [DisplayName("وزن ارزشی گزینه")]
        public  byte Weight { get; set; }
        #endregion  
    }
}