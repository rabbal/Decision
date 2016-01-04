using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.ViewModel.Appraiser;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارزیاب
    /// </summary>
    public interface IAppraiserService
    {
        /// <summary>
        /// واکشی ارزیاب برای ویرایش
        /// </summary>
        /// <param name="id">آی دی ارزیاب</param>
        /// <returns></returns>
        Task<EditAppraiserViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف ارزیاب
        /// </summary>
        /// <param name="id">آی دی ارزیاب</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش ارزیاب
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش ارزیاب</param>
        /// <returns></returns>
        Task EditAsync(EditAppraiserViewModel viewModel);

        /// <summary>
        /// درج ارزیاب جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج ارزیاب</param>
        Task<AppraiserViewModel> Create(AddAppraiserViewModel viewModel);

        /// <summary>
        /// نمایش لیست ارزیاب ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<AppraiserListViewModel> GetPagedListAsync(AppraiserSearchRequest request);

        Task<AddAppraiserViewModel> GetForCreate();

        Task FillAddViewModel(AddAppraiserViewModel viewModel);

        Task<bool> IsInDb(Guid id);

        Task FillEditViewModel(EditAppraiserViewModel viewModel);

        Task<AppraiserViewModel> GetAppraiserViewModel(Guid id);

        Task<IEnumerable<SelectListItem>> GetAsSelectedListItem(Guid? selected);
    }
}