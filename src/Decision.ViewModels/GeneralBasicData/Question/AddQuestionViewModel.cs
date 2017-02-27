using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels.GeneralBasicData.Question
{
    public class AddQuestionViewModel
    {
        #region Properties
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [DisplayName("عنوان")]
        public  string Title { get; set; }

        [DisplayName("وزن ارزش سوال")]
        [Required(ErrorMessage = "لطفا ورزن ارزشی سوال را مشخص کنید")]
        public  byte Weight { get; set; }

        public  IList<AddAnswerOptionViewModel> Options { get; set; }
        #endregion
    }
}