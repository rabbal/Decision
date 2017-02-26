using System;
using System.Collections.Generic;
using System.ComponentModel;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Question
{
    public class QuestionViewModel : BaseViewModel
    {
        #region Properties
        public Guid Id { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }
        [DisplayName("وزن ارزش سوال")]
        public byte Weight { get; set; }

        public  IList<AnswerOptionViewModel> Options { get; set; }
        #endregion 

    }
}
