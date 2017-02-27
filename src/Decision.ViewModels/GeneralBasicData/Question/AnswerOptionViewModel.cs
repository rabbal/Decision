using System;
using System.ComponentModel;

namespace Decision.ViewModels.GeneralBasicData.Question
{
    public class AnswerOptionViewModel
    {
        #region Properties
        public Guid Id { get; set; }
        [DisplayName("متن گزینه")]
        public string Name { get; set; }
        [DisplayName("وزن ارزشی گزینه")]
        public byte Weight { get; set; }
        #endregion
    }
}