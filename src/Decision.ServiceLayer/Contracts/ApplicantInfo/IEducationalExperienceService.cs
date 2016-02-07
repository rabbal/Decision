using System;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.EducationalExperience;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس سابقه آموزشی
    /// </summary>
    public interface IEducationalExperienceService
    {
        /// <summary>
        /// واکشی سابقه آموزشی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی سابقه آموزشی</param>
        /// <returns></returns>
        Task<EditEducationalExperienceViewModel> GetForEditAsync(Guid id);
        /// <summary>
        /// حذف سابقه آموزشی
        /// </summary>
        /// <param name="id">آی دی سابقه آموزشی</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش سابقه آموزشی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش سابقه آموزشی</param>
        /// <returns></returns>
        Task EditAsync(EditEducationalExperienceViewModel viewModel);
        /// <summary>
        /// درج سابقه آموزشی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج سابقه آموزشی</param>
        Task<EducationalExperienceViewModel> Create(AddEducationalExperienceViewModel viewModel);
        /// <summary>
        /// نمایش لیست سابقه های آموزشی با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<EducationalExperienceListViewModel> GetPagedListAsync(EducationalExperienceSearchRequest request);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        Task<EducationalExperienceViewModel> GetEducationalExperienceViewModel(Guid guid);
    }
}