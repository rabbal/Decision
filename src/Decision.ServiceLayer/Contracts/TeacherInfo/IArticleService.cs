using System;
using System.Threading.Tasks;
using Decision.ViewModel.Article;
using Decision.ViewModel.ArticleEvaluation;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس مقاله صادر شده توسط استاد
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// واکشی مقاله صادر شده توسط استاد برای ویرایش
        /// </summary>
        /// <param name="id">آی در مقاله صادر شده</param>
        /// <returns></returns>
        Task<EditArticleViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف مقاله صادر شده توسط استاد
        /// </summary>
        /// <param name="id">آی دی مقاله صادر شده توسط استاد</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش مقاله صادر شده توسط استاد
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش مقاله صادر شده توسط استاد</param>
        /// <returns></returns>
        Task EditAsync(EditArticleViewModel viewModel);

        /// <summary>
        /// درج مقاله صادر شده توسط استاد جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج مقاله صادر شده توسط استاد</param>
        Task<ArticleViewModel> Create(AddArticleViewModel viewModel);

        /// <summary>
        /// نمایش لیست مقاله های صادر شده توسط استاد با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<ArticleListViewModel> GetPagedListAsync(ArticleSearchRequest request);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task<ArticleViewModel> GetArticleViewModel(Guid guid);

        Task<byte[]> GetAttachment(Guid id);
        Guid GetTeacherId(Guid id);
        Task<ArticleDetails> GetDetailes(Guid id);
        long Count();
    }
}