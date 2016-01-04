using System;
using System.Threading.Tasks;
using Decision.ViewModel.EntireEvaluation;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارزیابی از استاد
    /// </summary>
    public interface IEntireEvaluationService
    {
        /// <summary>
        /// واکشی ارزیابی از استاد برای ویرایش
        /// </summary>
        /// <param name="id">آی دی ارزیابی استاد</param>
        /// <returns></returns>
        Task<EditEntireEvaluationViewModel> GetForEditAsync(Guid id);
        /// <summary>
        /// حذف ارزیابی از استاد
        /// </summary>
        /// <param name="id">آی دی ارزیابی از استاد</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش ارزیابی از استاد
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش ارزیابی از استاد</param>
        /// <returns></returns>
        Task EditAsync(EditEntireEvaluationViewModel viewModel);
        /// <summary>
        /// درج ارزیابی از استاد جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج ارزیابی از استاد</param>
        Task<EntireEvaluationViewModel> Create(AddEntireEvaluationViewModel viewModel);
        /// <summary>
        /// نمایش لیست ارزیابی های انجام شده از استاد ها با امکان جستجو و مرتب سازی
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

        Task<AddEntireEvaluationViewModel> GetForCreate(Guid TeacherId);

        Task FillEditViewModel(EditEntireEvaluationViewModel viewModel);

        Task FillAddViewModel(AddEntireEvaluationViewModel viewModel);

        Task<byte[]> GaetAttachment(Guid id);
    }
}