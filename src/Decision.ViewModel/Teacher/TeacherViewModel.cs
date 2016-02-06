﻿using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Applicant
{
    public class ApplicantViewModel :BaseViewModel
    {
        #region Properties

        public Guid Id { get; set; }
        /// <summary>
        /// نام متقاضی
        /// </summary>
        public  string FullName { get; set; }
        /// <summary>
        /// تاریخ تولد متقاضی
        /// </summary>
        public  DateTime BirthDate { get; set; }
        /// <summary>
        /// کد ملی متقاضی
        /// </summary>
        public  string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه متقاضی
        /// </summary>
        public  string BirthCertificateNumber { get; set; }
        /// <summary>
        /// پایه متقاضی
        /// </summary>
        public  int CollegiateOrder { get; set; }
        /// <summary>
        /// گروه شغلی متقاضی
        /// </summary>
        public  int OccupationalGroup { get; set; }
        /// <summary>
        /// لباس آخوندی پوشیده است یا خیر؟
        /// </summary>
        public  bool IsClothed { get; set; }
        /// <summary>
        /// معدل کار آموزی
        /// </summary>
        public  decimal TrainingGPA { get; set; }
        /// <summary>
        /// رتبه کارآموزی
        /// </summary>
        public  int TrainigGrade { get; set; }
        /// <summary>
        ///  عکس متقاضی
        /// </summary>
        public  byte[] Photo { get; set; }
        /// <summary>
        /// سنوات اداری
        /// </summary>
        public  int OfficialYears { get; set; }
        /// <summary>
        /// سنوات
        /// </summary>
        public  int CollegiateYears { get; set; }
        /// <summary>
        /// متقاضی برای ویرایش ، به یک کاربر دیگر ارجاع  داده شده است؟
        /// </summary>
        public  bool IsInReference { get; set; }
        /// <summary>
        /// آیا متقاضی توسط یکی از مدیران تایید شده است؟
        /// </summary>
        public  bool IsApproved { get; set; }
        /// <summary>
        /// شهر محل تولد
        /// </summary>
        public  string BirthPlaceCity { get; set; }
        /// <summary>
        /// استان محل تولد
        /// </summary>
        public  string BirthPlaceState { get; set; }
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonnelCode { get; set; }
        /// <summary>
        /// سمت متقاضی
        /// </summary>
        public  string PositionName { get; set; }
        /// <summary>
        /// نام کاربری مدیری که نگارش این متقاضی را تایید  کرده است
        /// </summary>
        public  string  ApproveByName { get; set; }
        #endregion
    }
}