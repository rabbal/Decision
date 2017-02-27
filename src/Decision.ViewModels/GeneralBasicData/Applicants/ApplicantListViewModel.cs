using System.Collections.Generic;
using System.Web.Mvc;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class ApplicantListViewModel : ListViewModelBase
    {
        public ApplicantListViewModel()
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
        public ApplicantListRequest Request { get; set; }
        public IList<ApplicantViewModel> Applicants { get; set; }
    }
}