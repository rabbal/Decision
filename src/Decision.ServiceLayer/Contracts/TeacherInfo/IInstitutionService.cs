using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.ViewModel.Institution;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس موسسه آموزشی
    /// </summary>
    public interface IInstitutionService
    {
        /// <summary>
        /// واکشی موسسه آموزشی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی موسسه آموزشی</param>
        /// <returns></returns>
        Task<EditInstitutionViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف موسسه آموزشی
        /// </summary>
        /// <param name="id">آی دی موسسه آموزشی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش موسسه آموزشی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش موسسه آموزشی</param>
        /// <returns></returns>
        Task EditAsync(EditInstitutionViewModel viewModel);

        /// <summary>
        /// درج موسسه آموزشی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج موسسه آموزشی</param>
        Task<InstitutionViewModel> Create(AddInstitutionViewModel viewModel);

        /// <summary>
        /// نمایش لیست موسسه آموزشی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<InstitutionListViewModel> GetPagedListAsync(InstitutionSearchRequest request);


        /// <summary>
        /// نمایش لیست موسسه آموزشی ها به صورت آبشاری 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(Guid ? selectedId);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task<InstitutionViewModel> GetInstitutionViewModel(Guid id);
    }
}