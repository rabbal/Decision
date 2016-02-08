using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    ///معرف یک متقاضی
    /// </summary>
    public class Applicant : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// create one instance Of <see cref="Applicant"/>
        /// </summary>
        public Applicant()
        {
            Photo = BitConverter.GetBytes(0);
            CopyOfBirthCertificate = Photo = BitConverter.GetBytes(0);
            CopyOfNationalCard = Photo = BitConverter.GetBytes(0);
            Status = ApplicantStatus.Pending;
        }
        #endregion

        #region Properties
        /// <summary>
        /// نام متقاضی
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی متقاضی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        public string FatherName { get; set; }
        /// <summary>
        /// مذهب
        /// </summary>
        public string Gilder { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string CellphoneNumber { get; set; }
        /// <summary>
        /// شماره تلفن ضروری
        /// </summary>
        public string NumberIndispensable { get; set; }
        /// <summary>
        /// وضعیت نظام وظیفه
        /// </summary>
        public MilitaryStatus MilitaryStatus { get; set; }
        /// <summary>
        /// تاریخ پایان خدمت
        /// </summary>
        public DateTime? ServedEndOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MembershipType MembershipType { get; set; }
        /// <summary>
        /// تاریخ تولد متقاضی
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کد ملی متقاضی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه متقاضی
        /// </summary>
        public string BirthCertificateNumber { get; set; }
        /// <summary>
        /// وضعیت تأهل متقاضی
        /// </summary>
        public MarriageStatus MarriageStatus { get; set; }
        /// <summary>
        /// جنسیت متقاضی
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        ///  عکس متقاضی
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
        /// شهر محل تولد
        /// </summary>
        public string BirthPlaceCity { get; set; }
        /// <summary>
        /// استان محل تولد
        /// </summary>
        public string BirthPlaceState { get; set; }
        /// <summary>
        /// کل شهرت متقاضی
        /// </summary>
        public double TotalReputation { get; set; }
        /// <summary>
        /// وضعیت رسیدگی
        /// </summary>
        public ApplicantStatus Status { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// مقاله هایی که متقاضی صادر کرده است
        /// </summary>
        public ICollection<Article> Articles { get; set; }
        /// <summary>
        /// سوابق تدریس متقاضی
        /// </summary>
        public ICollection<EducationalExperience> EducationalExperiences { get; set; }
        /// <summary>
        /// سوابق تحقیقات متقاضی
        /// </summary>
        public ICollection<ResearchExperience> ReseachExperiences { get; set; }
        /// <summary>
        /// ارزیابی هایی که از متقاضی به عمل آماده
        /// </summary>
        public ICollection<EntireEvaluation> EntireEvaluations { get; set; }
        /// <summary>
        /// سوابق تحصیلی متقاضی
        /// </summary>
        public ICollection<EducationalBackground> EducationalBackgrounds { get; set; }
        /// <summary>
        /// سوابق کاری متقاضی
        /// </summary>
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        /// <summary>
        /// مصاحبه های بعمل آمده از متقاضی
        /// </summary>
        public ICollection<Interview> Interviews { get; set; }
        /// <summary>
        /// آدرس های متقاضی
        /// </summary>
        public ICollection<Address> Addresses { get; set; }
        /// <summary>
        /// معرفان متقاضی
        /// </summary>
        public ICollection<Presenter> Presenters { get; set; }
        #endregion
    }
}
