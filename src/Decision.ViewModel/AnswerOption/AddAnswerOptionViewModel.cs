using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModel.AnswerOption
{
    /// <summary>
    /// ویومدل درج گزینه برای سوال چند گزینه ای
    /// </summary>
    public class AddAnswerOptionViewModel
    {
        #region Properties
        /// <summary>
        /// نام گزینه پاسخ
        /// </summary>
        [DisplayName("متن گزینه")]
        [Required(ErrorMessage = "لطفا متن گزینه را وارد کنید")]
        [StringLength(1024,  ErrorMessage = "متن گزینه نباید بیشتر از ۱۰۲۴  کاراکتر باشد")]
        
        public  string Name { get; set; }
        /// <summary>
        /// وزن ارزشی گزینه
        /// </summary>
        [DisplayName("وزن ارزشی گزینه")]
        [Required(ErrorMessage = "لطفا وزن ارزشی گزینه را وارد کنید")]
        public  byte Weight { get; set; }
        #endregion 
    }
}