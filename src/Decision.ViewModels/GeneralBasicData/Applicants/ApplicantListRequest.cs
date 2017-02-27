using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Decision.DomainClasses.Applicants;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class ApplicantListRequest : ListRequestBase
    {
        public ApplicantListRequest()
        {
            SortByItems = new List<SelectListItem>
            {
                new SelectListItem {Value = nameof(Applicant.FirstName),Text = "نام"},
                new SelectListItem {Value = nameof(Applicant.LastName),Text = "نام خانوادگی"},
                new SelectListItem {Value = nameof(Applicant.CreationDateTime),Text = "تاریخ ایجاد"},
                new SelectListItem {Value = nameof(Applicant.LasModificationDateTime),Text = "تاریخ ویرایش"},
                new SelectListItem {Value =nameof(ApplicantScore.Score),Text = "امتیاز در دوره"}
            };
        }

        [DisplayName("نام")]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }
        [DisplayName("شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }
    }
}