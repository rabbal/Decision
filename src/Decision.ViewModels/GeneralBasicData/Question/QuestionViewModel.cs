using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Decision.ViewModels.GeneralBasicData.Question
{
    public class QuestionViewModel 
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
