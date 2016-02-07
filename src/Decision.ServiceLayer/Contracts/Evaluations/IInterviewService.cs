using System;
using System.Threading.Tasks;
using Decision.ViewModel.Interview;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس مصاحبه
    /// </summary>
    public interface IInterviewService
    {
        /// <summary>
        /// واکشی مصاحبه برای ویرایش
        /// </summary>
        /// <param name="id">آی دی مصاحبه</param>
        /// <returns></returns>
        Task<EditInterviewViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف مصاحبه
        /// </summary>
        /// <param name="id">آی دی مصاحبه</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش مصاحبه
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش مصاحبه</param>
        /// <returns></returns>
        Task EditAsync(EditInterviewViewModel viewModel);

        /// <summary>
        /// درج مصاحبه جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج مصاحبه</param>
        Task<InterviewViewModel> Create(AddInterviewViewModel viewModel);

        /// <summary>
        /// نمایش لیست مصاحبه ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<InterviewListViewModel> GetPagedListAsync(InterviewSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        
    }
}