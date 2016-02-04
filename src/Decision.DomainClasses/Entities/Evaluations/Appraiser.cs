using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده ارزش گذار از قبیل  ارزیاب/ مصاحبه کننده و غیره..
    /// </summary>
    public class Appraiser : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Appraiser()
        {
            Gender=GenderType.Male;
        }
        #endregion

        #region Properties
        /// <summary>
        /// نام ارزیاب
        /// </summary>
        public  string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی ارزیاب
        /// </summary>
        public  string LastName { get; set; }
        /// <summary>
        /// شماره همراه ارزیاب
        /// </summary>
        public  string CellPhone { get; set; }
        /// <summary>
        /// جنسیت ارزیاب
        /// </summary>
        public  GenderType Gender { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// ارزیابی های به عمل آمده از اساتید
        /// </summary>
        public  ICollection<EntireEvaluation> EntireEvaluations  { get; set; }
        /// <summary>
        /// ارزیابی های به عمل آمده از مقاله استاد ها
        /// </summary>
        public  ICollection<ArticleEvaluation> ArticlesEvaluations { get; set; }
        /// <summary>
        /// مصاحبات انجام داده از اساتید
        /// </summary>
        public  ICollection<Interview> Interviews{ get; set; }
        /// <summary>
        /// آی دی عنوان  ارزیاب  
        /// مهندس/دکتر/...
        /// </summary>
        public  Guid AppraiserTitleId { get; set; }
        /// <summary>
        /// عنوان  ارزیاب
        /// مهندس/دکتر/...
        /// </summary>
        public  Title AppraiserTitle { get; set; }
        #endregion
    }
}
