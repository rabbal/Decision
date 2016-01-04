using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.Question;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس سوال ها
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id">آی دی سوال</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// واکشی سوال برای ویرایش
        /// </summary>
        /// <param name="id">آی دی سوال</param>
        /// <returns></returns>
        Task<EditQuestionViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش سوال</param>
        /// <returns></returns>
        Task EditAsync(EditQuestionViewModel viewModel);

        /// <summary>
        /// درج سوال جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج سوال</param>
        Task<QuestionViewModel> CreateAsync(AddQuestionViewModel viewModel);

        /// <summary>
        /// نمایش لیست ارزیاب ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<QuestionListViewModel> GetPagedListAsync(QuestionSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        /// <summary>
        /// دریافت ویومدل نمایش سوال
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<QuestionViewModel> GetQuestionViewModel(Guid id);

        Task DisableAsync(Guid guid);

        Task EnableAsync(Guid guid);
        Task<IEnumerable<Question>> GetQuestionsByIdsAsync(IEnumerable<Guid> ids);

        Task<IEnumerable<Question>> GelAllActive();
    }
}