using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels.Question
{
    public class EditQuestionViewModel : BaseIsDelete
    {
        #region Properties
        public  Guid Id { get; set; }
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [DisplayName("عنوان")]
        public  string Title { get; set; }
        [DisplayName("وزن ارزش سوال")]
        public  byte Weight { get; set; }

        public  IList<EditAnswerOptionViewModel> Options { get; set; }
        #endregion 
    }
}