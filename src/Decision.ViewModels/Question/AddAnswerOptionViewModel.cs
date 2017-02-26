using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels.Question
{
    public class AddAnswerOptionViewModel
    {
        #region Properties
        [DisplayName("متن گزینه")]
        [Required(ErrorMessage = "لطفا متن گزینه را وارد کنید")]
        [StringLength(1024,  ErrorMessage = "متن گزینه نباید بیشتر از ۱۰۲۴  کاراکتر باشد")]
        
        public  string Name { get; set; }
        [DisplayName("وزن ارزشی گزینه")]
        [Required(ErrorMessage = "لطفا وزن ارزشی گزینه را وارد کنید")]
        public  byte Weight { get; set; }
        #endregion 
    }
}