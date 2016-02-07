using System;
using System.Threading.Tasks;
using Decision.ViewModel.Article;
namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس مقاله صادر شده توسط متقاضی
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// واکشی مقاله صادر شده توسط متقاضی برای ویرایش
        /// </summary>
        /// <param name="id">آی در مقاله صادر شده</param>
        /// <returns></returns>
        Task<EditArticleViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف مقاله صادر شده توسط متقاضی
        /// </summary>
        /// <param name="id">آی دی مقاله صادر شده توسط متقاضی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش مقاله صادر شده توسط متقاضی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش مقاله صادر شده توسط متقاضی</param>
        /// <returns></returns>
        Task EditAsync(EditArticleViewModel viewModel);

        /// <summary>
        /// درج مقاله صادر شده توسط متقاضی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج مقاله صادر شده توسط متقاضی</param>
        Task<ArticleViewModel> Create(AddArticleViewModel viewModel);

        /// <summary>
        /// نمایش لیست مقاله های صادر شده توسط متقاضی با امکان جستجو و مرتب سازی
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
        Guid GetApplicantId(Guid id);
        long Count();
    }
}