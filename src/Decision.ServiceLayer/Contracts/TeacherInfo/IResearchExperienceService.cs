using System;
using System.Threading.Tasks;
using Decision.ViewModel.ResearchExperience;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    public interface IResearchExperienceService
    {
        /// <summary>
        /// واکشی سابقه پژوهشی برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditResearchExperienceViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف سابقه پژوهشی
        /// </summary>
        /// <param name="id">آی دی سابقه پژوهشی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش سابقه پژوهشی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش سابقه پژوهشی</param>
        /// <returns></returns>
        Task EditAsync(EditResearchExperienceViewModel viewModel);

        /// <summary>
        /// درج سابقه پژوهشی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج سابقه پژوهشی</param>
        Task<ResearchExperienceViewModel> Create(AddResearchExperienceViewModel viewModel);

        /// <summary>
        /// نمایش لیست سابقه های پژوهشی با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<ResearchExperienceListViewModel> GetPagedListAsync(ResearchExperienceSearchRequest request);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task<ResearchExperienceViewModel> GetResearchExperienceViewModel(Guid guid);
    }
}