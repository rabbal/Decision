using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// مشخص کننده انواع آگاه سازی ها
    /// </summary>
    public enum  NotificationType
    {
        /// <summary>
        /// گفتگوی جدید
        /// </summary>
        [Display(Name = "گفتگوی جدید")]
        NewConversation,
        /// <summary>
        /// پاسخ جدید به گفتگو
        /// </summary>
        [Display(Name = "پاسخ جدید به گفتگو")]
        NewConversationReply,
    }
}
