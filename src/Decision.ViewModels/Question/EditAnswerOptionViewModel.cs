using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels.Question
{
    public class EditAnswerOptionViewModel 
    {
        #region Properties
        public Guid Id { get; set; }

        [DisplayName("متن گزینه")]
        [Required(ErrorMessage = "لطفا متن پاسخ را وارد کنید")]
        [StringLength(1204, ErrorMessage = "متن گزینه نباید بیشتر از ۱۰۲۴  کاراکتر باشد")]
        
        public  string Name { get; set; }

        [DisplayName("وزن ارزشی گزینه")]
        public  byte Weight { get; set; }
        #endregion  
    }
}