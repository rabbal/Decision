using System;
using System.Threading.Tasks;
using Decision.ViewModel.WorkExperience;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس سابقه های کاری
    /// </summary>
    public interface IWorkExperienceService
    {
        /// <summary>
        /// واکشی سابقه کاری برای ویرایش
        /// </summary>
        /// <param name="id">آی دی سابقه کاری</param>
        /// <returns></returns>
        Task<EditWorkExperienceViewModel> GetForEditAsync(Guid id, string path);

        /// <summary>
        /// حذف سابقه کاری
        /// </summary>
        /// <param name="id">آی دی سابقه کاری</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش سابقه کاری
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش سابقه کاری</param>
        /// <returns></returns>
        Task EditAsync(EditWorkExperienceViewModel viewModel);

        /// <summary>
        /// درج سابقه کاری جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج سابقه کاری</param>
        Task<WorkExperienceViewModel> Create(AddWorkExperienceViewModel viewModel);

        /// <summary>
        /// نمایش لیست سابقه های کاری با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<WorkExperienceListViewModel> GetPagedListAsync(WorkExperienceSearchRequest request);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task<AddWorkExperienceViewModel> GetForCreate(Guid ApplicantId, string path);

        Task FillAddViewModel(AddWorkExperienceViewModel viewModel, string path);

        Task FillEditViewModel(EditWorkExperienceViewModel viewModel, string path);

        Task<WorkExperienceViewModel> GetWorkExperienceViewModel(Guid guid);
    }
}