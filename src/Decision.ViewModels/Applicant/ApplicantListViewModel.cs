using System.Collections.Generic;
using System.Web.Mvc;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Applicant
{
    public class ApplicantListViewModel
    {
        public ApplicantListViewModel()
        {
            Cities = new List<SelectListItem>();

            #region SortOrderList
            SortOrderList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = SortDirectionMode.Desc,
                    Text = "نزولی"
                    
                }
                ,
                new SelectListItem
                {
                    Value = SortDirectionMode.Asc,
                    Text = "صعودی"
                }
            };
            #endregion

            #region SortableList

            SortableList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = SortByMode.CreatedOn,
                    Text = "تاریخ درج"
                },
                new SelectListItem
                {
                    Value = SortByMode.ModifiedOn,
                    Text = "تاریخ آخرین تغییر"
                },
                new SelectListItem
                {
                    Value = ApplicantSortBy.FirstName,
                    Text = "نام"
                },
                new SelectListItem
                {
                    Value = ApplicantSortBy.LastName,
                    Text = "نام خانوادگی"
                }
            };

            #endregion

            #region PageSizeList

            PageSizeList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "10",
                    Text = "۱۰"
                },
                new SelectListItem
                {
                    Value = "20",
                    Text = "۲۰"
                },
                new SelectListItem
                {
                    Value = "30",
                    Text = "۳۰"
                },
                new SelectListItem
                {
                    Value = "50",
                    Text = "۵۰"
                },
                new SelectListItem
                {
                    Value = "100",
                    Text = "۱۰۰"
                }
            };

            #endregion

        }

        public ApplicantListRequest SearchRequest { get; set; }

        public IEnumerable<ApplicantViewModel> Applicants { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
       
        public IEnumerable<SelectListItem> SortableList { get; set; }
        public IEnumerable<SelectListItem> SortOrderList { get; set; }
        public IEnumerable<SelectListItem> PageSizeList { get; set; }
    }
}