using System;
using System.Threading.Tasks;
using Decision.ViewModel.ApplicantInServiceCourseType;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس تعداد ساعت یک نوع ضمن خدمت برای متقاضی
    /// </summary>
    public interface IApplicantInServiceCourseTypeService
    {
        /// <summary>
        /// واکشی تعداد ساعت یک نوع ضمن خدمت برای متقاضی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی تعداد ساعت یک نوع ضمن خدمت برای متقاضی</param>
        /// <returns></returns>
        Task<EditApplicantInServiceCourseTypeViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف تعداد ساعت یک نوع ضمن خدمت برای متقاضی
        /// </summary>
        /// <param name="id">آی دی تعداد ساعت یک نوع ضمن خدمت برای متقاضی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش تعداد ساعت یک نوع ضمن خدمت برای متقاضی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش تعداد ساعت یک نوع ضمن خدمت برای متقاضی</param>
        /// <returns></returns>
        Task EditAsync(EditApplicantInServiceCourseTypeViewModel viewModel);

        /// <summary>
        /// درج تعداد ساعت یک نوع ضمن خدمت برای متقاضی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج تعداد ساعت یک نوع ضمن خدمت برای متقاضی</param>
        Task<ApplicantInServiceCourseTypeViewModel> Create(AddApplicantInServiceCourseTypeViewModel viewModel);

        /// <summary>
        /// نمایش لیست تعداد ساعت انواع ضمن خدمت برای متقاضی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<ApplicantInServiceCourseTypeListViewModel> GetPagedListAsync(ApplicantInServiceCourseTypeSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task FillEditViewModel(EditApplicantInServiceCourseTypeViewModel viewModel);

        Task<ApplicantInServiceCourseTypeViewModel> GetApplicantInServiceCourseTypeViewModel(Guid guid);

        Task FillAddViewModel(AddApplicantInServiceCourseTypeViewModel viewModel);

        Task<AddApplicantInServiceCourseTypeViewModel> GetForCreate(Guid ApplicantId);
    }
}