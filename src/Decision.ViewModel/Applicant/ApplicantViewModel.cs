using System;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Applicant
{
    public class ApplicantViewModel : BaseViewModel
    {
        #region Properties

        public Guid Id { get; set; }
        /// <summary>
        /// نام متقاضی
        /// </summary>
        public string FullName { get; set; }
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
        ///  عکس متقاضی
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// شهر محل تولد
        /// </summary>
        public string BirthPlaceCity { get; set; }
        /// <summary>
        /// استان محل تولد
        /// </summary>
        public string BirthPlaceState { get; set; }
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
        /// وضعیت تأهل متقاضی
        /// </summary>
        public MarriageStatus MarriageStatus { get; set; }
        /// <summary>
        /// جنسیت متقاضی
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// کل شهرت متقاضی
        /// </summary>
        public double TotalReputation { get; set; }
        /// <summary>
        /// وضعیت رسیدگی
        /// </summary>
        public ApplicantStatus Status { get; set; }
        #endregion 
    }
}