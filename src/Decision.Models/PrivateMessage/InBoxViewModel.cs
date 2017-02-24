using System;

namespace Decision.ViewModel.PrivateMessage
{
    public class InBoxViewModel
    {
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// آیا گفتگوی جدید مشاهده شده است؟
        /// </summary>
        public bool IsSeen { get; set; }
        /// <summary>
        /// موضوع گفتگو
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// تاریخ آغاز گفتگو
        /// </summary>
        public DateTime SentOn { get; set; }
        /// <summary>
        /// نام کاربری ارسال کننده
        /// </summary>
        public string DisplayName { get; set; }
    }
}
