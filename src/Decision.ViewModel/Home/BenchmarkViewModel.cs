using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.ViewModel.Home
{
   public  class BenchmarkViewModel
   {
       /// <summary>
       /// تعداد کل کاربران
       /// </summary>
       public string UsersCount { get; set; } = "۰";
        /// <summary>
        /// تعداد اساتیدی که برای مدیریت ارسال شده اند ولی هنوز تایید نشده اند
        /// </summary>
       public string  NonApprovedApplicantsCount  { get; set; } = "۰";
        /// <summary>
        /// تاریخ آخرین بک آپ گیری
        /// </summary>
        public string LastBackupDate { get; set; } = "../../..";
        /// <summary>
        /// تعداد کل اساتید ثبت شده و یا ارسال شده به مدیریت
        /// </summary>
        public string JugesCount { get; set; } = "۰";
        /// <summary>
        /// تعداد اساتید تأیید شده
        /// </summary>
        public string ApprovedApplicantsCount { get; set; } = "۰";
        /// <summary>
        /// تعداد کل مقالات
        /// </summary>
        public string ArticlesCount { get; set; } = "۰";
        /// <summary>
        /// تعداد کل ارزیابی های مقالات
        /// </summary>
        public string ArticleEvaluationsCount { get; set; } = "۰";
        /// <summary>
        /// تاریخ آخرین وظیفه انجام شده برای پاکسازی دیتابیس از داده های زاید
        /// </summary>
        public string DateOfLastTask { get; set; } = "۰۰/۰۰/۰۰";
        /// <summary>
        /// وضعیت آخرین وظیفه انجام شده
        /// </summary>
        public string StatusOfLastTask { get; set; } = "-";
        /// <summary>
        /// زمان باقی مانده تا وظیفه بعدی
        /// </summary>
        public string RemainingDateToNextTask { get; set; } = "۰";
        /// <summary>
        /// تعداد خطا های سیستم
        /// </summary>
        public string SystemErrorsCount { get; set; } = "۰";
    }
}
