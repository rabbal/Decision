using System;
using System.Threading.Tasks;
using Decision.ViewModel.EducationalBackground;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس سابقه تحصیلی
    /// </summary>
    public interface IEducationalBackgroundService
    {
        /// <summary>
        /// واکشی سابقه تحصیلی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی سابقه تحصیلی</param>
        /// <returns></returns>
        Task<EditEducationalBackgroundViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف سابقه تحصیلی
        /// </summary>
        /// <param name="id">آی دی سابقه تحصیلی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش سابقه تحصیلی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش سابقه تحصیلی</param>
        /// <returns></returns>
        Task EditAsync(EditEducationalBackgroundViewModel viewModel);

        /// <summary>
        /// درج سابقه تحصیلی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج سابقه تحصیلی</param>
        Task<EducationalBackgroundViewModel> Create(AddEducationalBackgroundViewModel viewModel);

        /// <summary>
        /// نمایش لیست سوابق تحصیلی با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<EducationalBackgroundListViewModel> GetPagedListAsync(EducationalBackgroundSearchRequest request);


        Task<bool> IsInDb(Guid guid);
        
    }
}