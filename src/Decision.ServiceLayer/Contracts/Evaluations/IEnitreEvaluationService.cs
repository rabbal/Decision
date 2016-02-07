using System;
using System.Threading.Tasks;
using Decision.ViewModel.EntireEvaluation;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارزیابی از متقاضی
    /// </summary>
    public interface IEntireEvaluationService
    {
        /// <summary>
        /// واکشی ارزیابی از متقاضی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی ارزیابی متقاضی</param>
        /// <returns></returns>
        Task<EditEntireEvaluationViewModel> GetForEditAsync(Guid id);
        /// <summary>
        /// حذف ارزیابی از متقاضی
        /// </summary>
        /// <param name="id">آی دی ارزیابی از متقاضی</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش ارزیابی از متقاضی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش ارزیابی از متقاضی</param>
        /// <returns></returns>
        Task EditAsync(EditEntireEvaluationViewModel viewModel);
        /// <summary>
        /// درج ارزیابی از متقاضی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج ارزیابی از متقاضی</param>
        Task<EntireEvaluationViewModel> Create(AddEntireEvaluationViewModel viewModel);
        /// <summary>
        /// نمایش لیست ارزیابی های انجام شده از متقاضی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<EntireEvaluationListViewModel> GetPagedListAsync(EntireEvaluationSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

      
    }
}