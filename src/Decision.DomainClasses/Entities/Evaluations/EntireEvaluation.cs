using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده ارزیابی از خود استاد
    /// </summary>
    public class EntireEvaluation : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public EntireEvaluation()
        {
            Attachment = BitConverter.GetBytes(0);
        }
        #region Properties
        /// <summary>
        ///  نظریه کلی برای استاد
        /// </summary>
        public  string Content { get; set; }
        /// <summary>
        /// تاریخ ارزیابی
        /// </summary>
        public  DateTime EvaluationDate { get; set; }
        /// <summary>
        /// خلاصه ارزیابی 
        /// </summary>
        public  string Brief { get; set; }
        /// <summary>
        /// نقاط ضعف استاد
        /// </summary>
        public  string Foible { get; set; }
        /// <summary>
        /// نقطه قوت استاد
        /// </summary>
        public  string StrongPoint { get; set; }
        /// <summary>
        /// فایل ضمیمه ارزیابی
        /// </summary>
        public  byte[] Attachment { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی استاد ارزیابی شده
        /// </summary>
        public  Guid TeacherId { get; set; }
        /// <summary>
        /// استاد ارزیابی شده
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// ارزیاب  استاد
        /// </summary>
        public  Appraiser Evaluator  { get; set; }
        /// <summary>
        /// آی دی ارزیاب
        /// </summary>
        public  Guid EvaluatorId { get; set; }
      

        #endregion
    }
}
