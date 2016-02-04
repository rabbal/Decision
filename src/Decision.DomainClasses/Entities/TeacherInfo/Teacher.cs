using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    ///معرف یک استاد
    /// </summary>
    public class Teacher : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Teacher()
        {
            IsApproved = false;
            IsInReference = true;

            Gender = GenderType.Male;
            Photo = BitConverter.GetBytes(0);
            CopyOfBirthCertificate = Photo = BitConverter.GetBytes(0);
            CopyOfNationalCard = Photo = BitConverter.GetBytes(0);

        }
        #endregion

        #region Properties
        /// <summary>
        /// نام استاد
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی استاد
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// تاریخ تولد استاد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کد ملی استاد
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه استاد
        /// </summary>
        public string BirthCertificateNumber { get; set; }
        /// <summary>
        /// وضعیت تأهل استاد
        /// </summary>
        public MarriageStatus MarriageStatus { get; set; }
        /// <summary>
        /// جنسیت استاد
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        ///  عکس استاد
        /// </summary>
        public byte[] Photo { get; set; }
        /// <summary>
        ///  کپی کارت ملی
        /// </summary>
        public byte[] CopyOfNationalCard { get; set; }
        /// <summary>
        ///  کپی شناسنامه 
        /// </summary>
        public byte[] CopyOfBirthCertificate { get; set; }
        /// <summary>
        /// استاد برای ویرایش ، به یک کاربر دیگر ارجاع  داده شده است؟
        /// </summary>
        public bool IsInReference { get; set; }
        /// <summary>
        /// آیا استاد توسط یکی از مدیران تایید شده است؟
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// شهر محل تولد
        /// </summary>
        public string BirthPlaceCity { get; set; }
        /// <summary>
        /// استان محل تولد
        /// </summary>
        public string BirthPlaceState { get; set; }
        /// <summary>
        /// امتیاز
        /// </summary>
        public double Score { get; set; }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// مقاله هایی که استاد صادر کرده است
        /// </summary>
        public ICollection<Article> Articles { get; set; }
        /// <summary>
        /// سوابق تدریس استاد
        /// </summary>
        public ICollection<EducationalExperience> EducationalExperiences { get; set; }
        /// <summary>
        /// سوابق تحقیقات استاد
        /// </summary>
        public ICollection<ResearchExperience> ReseachExperiences { get; set; }
        /// <summary>
        /// ارزیابی هایی که از استاد به عمل آماده
        /// </summary>
        public ICollection<EntireEvaluation> EntireEvaluations { get; set; }
        /// <summary>
        /// سوابق تحصیلی استاد
        /// </summary>
        public ICollection<EducationalBackground> EducationalBackgrounds { get; set; }
        /// <summary>
        /// سوابق کاری استاد
        /// </summary>
        public ICollection<WorkExperience> WorkExperiences { get; set; }

        /// <summary>
        /// مصاحبه های بعمل آمده از استاد
        /// </summary>
        public ICollection<Interview> Interviews { get; set; }
        /// <summary>
        /// آی دی مدیری که نگارش این استاد را تایید  کرده است
        /// </summary>
        public Guid? ApproveById { get; set; }
        /// <summary>
        ///  مدیری که نگارش  این استاد را تایید  کرده است
        /// </summary>
        public User ApproveBy { get; set; }
        /// <summary>
        /// لیست ارجاعاتی که استاد در آنها برای ویرایش ارجاع داده شده است
        /// </summary>
        public ICollection<ReferentialTeacher> ReferentialTeachers { get; set; }
        /// <summary>
        /// آدرس های استاد
        /// </summary>
        public ICollection<Address> Addresses { get; set; }
        #endregion
    }
}
