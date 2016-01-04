using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.AnswerOption;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس گزینه های سوال
    /// </summary>
    public interface IAnswerOptionService
    {
        /// <summary>
        /// حذف گزینه ی سوال
        /// </summary>
        /// <param name="id">آی دی گزینه ی سوال</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش گزینه ی سوال
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش گزینه ی سوال</param>
        /// <returns></returns>
        Task EditAsync(EditAnswerOptionViewModel viewModel);

        /// <summary>
        /// درج گزینه ی سوال جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج گزینه ی سوال</param>
        Task<AnswerOptionViewModel> Create(AddAnswerOptionViewModel viewModel);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        /// <summary>
        /// دریافت ویومدل نمایش گزینه ی سوال
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<AnswerOptionViewModel> GetAnswerOptionViewModel(Guid id);

        Task<IEnumerable<AnswerOption>> GetAnswerOptionsByIds(IEnumerable<Guid> ids);
    }
}