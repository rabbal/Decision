using System;
using System.Threading.Tasks;
using Decision.ViewModel.TeacherInServiceCourseType;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس تعداد ساعت یک نوع ضمن خدمت برای استاد
    /// </summary>
    public interface ITeacherInServiceCourseTypeService
    {
        /// <summary>
        /// واکشی تعداد ساعت یک نوع ضمن خدمت برای استاد برای ویرایش
        /// </summary>
        /// <param name="id">آی دی تعداد ساعت یک نوع ضمن خدمت برای استاد</param>
        /// <returns></returns>
        Task<EditTeacherInServiceCourseTypeViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف تعداد ساعت یک نوع ضمن خدمت برای استاد
        /// </summary>
        /// <param name="id">آی دی تعداد ساعت یک نوع ضمن خدمت برای استاد</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش تعداد ساعت یک نوع ضمن خدمت برای استاد
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش تعداد ساعت یک نوع ضمن خدمت برای استاد</param>
        /// <returns></returns>
        Task EditAsync(EditTeacherInServiceCourseTypeViewModel viewModel);

        /// <summary>
        /// درج تعداد ساعت یک نوع ضمن خدمت برای استاد جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج تعداد ساعت یک نوع ضمن خدمت برای استاد</param>
        Task<TeacherInServiceCourseTypeViewModel> Create(AddTeacherInServiceCourseTypeViewModel viewModel);

        /// <summary>
        /// نمایش لیست تعداد ساعت انواع ضمن خدمت برای استاد ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<TeacherInServiceCourseTypeListViewModel> GetPagedListAsync(TeacherInServiceCourseTypeSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        Task FillEditViewModel(EditTeacherInServiceCourseTypeViewModel viewModel);

        Task<TeacherInServiceCourseTypeViewModel> GetTeacherInServiceCourseTypeViewModel(Guid guid);

        Task FillAddViewModel(AddTeacherInServiceCourseTypeViewModel viewModel);

        Task<AddTeacherInServiceCourseTypeViewModel> GetForCreate(Guid TeacherId);
    }
}