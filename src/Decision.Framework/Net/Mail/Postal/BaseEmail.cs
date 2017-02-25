namespace Decision.Framework.Net.Mail.Postal
{
    /// <summary>
    ///     از ایمیل‌های خودکار برنامه log تهیه کنید.
    ///     <example>ثبت در بانک اطلاعاتی</example>
    /// </summary>
    public abstract class BaseEmail : Email
    {
        protected BaseEmail()
        {
            SentOn = PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime).GetPersianNumbers();
        }

        public string To { get; set; }

        /// <summary>
        ///     بهتر است داینامیک باشد
        ///     <example>sample: ‫"ارجاع کار جدید: از طرف : ... ، موضوع: ... ، درجه اهمیت: ...است" ‎‎‎‎‎‎‎‎‎</example>
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     به صورت تاریخ شمسی
        /// </summary>
        public string SentOn { get; set; }

        /// <summary>
        /// حداکثر 100 رونوشت مخفی
        /// بهتر از ارسال 100 ایمیل جدا
        /// </summary>
        public string BCC { get; set; }

        /// <summary>
        ///     دقیقا مشخص کنید که ایمیل دریافتی آیا رونوشت‌ است یا خیر!
        ///     <example>
        ///         علاوه بر ذکر این مورد، قبل از بدنه اصلی ایمیل مشخص کنید که این ایمیل یک رونوشت است
        ///     </example>
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        ///     هر چه می‌توانید اطلاعات بیشتری را توسط یک ایمیل خودکار منتقل کنید
        ///     <example>
        ///         ذکر "ارجاع کار جدید ..." در عنوان و سپس مجددا
        ///         ذکر همین عنوان به عنوان بدنه‌ی ایمیل خودکار به زودی ایمیل‌های شما را تبدیل به نوعی
        ///         Spam آزار دهنده خواهد کرد
        ///     </example>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     sender , from , replyto
        ///     + from , username  در وب کانفیگ
        ///     این 5 مورد اگر یکی باشند احتمال اسپم خیلی کم است
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        ///     به چه ایمیلی پاسخ ارسال شود؟
        /// </summary>
        public string ReplyTo { get; set; }

        public string From { get; set; }

        public string SiteShortName { get; set; }
    }
}