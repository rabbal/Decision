using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ViewModel.Home;
using Decision.ViewModel.Teacher;
using Decision.ViewModel.ReferentialTeacher;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس استاد
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// واکشی استاد برای ویرایش
        /// </summary>
        /// <param name="id">آی دی استاد</param>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task<EditTeacherViewModel> GetForEditAsync(Guid id, string path);
        /// <summary>
        /// حذف استاد
        /// </summary>
        /// <param name="id">آی دی استاد</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش استاد
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش استاد</param>
        /// <returns></returns>
        Task EditAsync(EditTeacherViewModel viewModel);
        /// <summary>
        /// درج استاد جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج استاد</param>
        void Create(AddTeacherViewModel viewModel);
        /// <summary>
        /// نمایش لیست استاد ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<TeacherListViewModel> GetPagedListAsync(TeacherSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        /// <summary>
        /// چک کردن موجود بودن کدملی استاد
        /// </summary>
        /// <param name="nationalCode">کدملی</param>
        /// <param name="id">آی دی <c>میتواند نال باشد</c></param>
        /// <returns></returns>
        Task<bool> IsTeacherNationalCodeExist(string nationalCode, Guid? id);

        /// <summary>
        /// چک کردن موجود بودن شماره شناسنامه استاد
        /// </summary>
        /// <param name="birthCertificateNumber">شماره شناسنامه</param>
        /// <param name="id">آی دی <c>میتواند نال باشد</c></param>
        /// <returns></returns>
        Task<bool> IsTeacherBirthCertificateNumberExist(string birthCertificateNumber, Guid? id);

        /// <summary>
        /// مقدار دهی لیست های مربوط به ویو مدل ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task FillEditViewMoel(EditTeacherViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست های مربوط به ویو مدل درج
        /// </summary>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task FillAddViewMoel(AddTeacherViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست ها مربوطه
        /// </summary>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task<AddTeacherViewModel> GetForCreate(string path);
        Task<byte[]> GetTeacherDocument(Guid id,string type);
        Task<TeacherDetailsViewModel> GetTeacherDetails(Guid id);
        Task<TeacherViewModel> Approve(Guid id);

        Task<TeacherViewModel> ReferTeacher(AddReferentialTeacherViewModel viewModel);
        Task<TeacherViewModel> CancelRefer(Guid id);

        Task<bool> IsInRefer(Guid guid);
        Task<IEnumerable<TeacherViewModel>> GetRefersTeachers();
        Task<IEnumerable<ReferTeacherViewModel>> GetRefersTeachers(bool withReferer);
        Task FinishedRefer(Guid id);
        long Count();
        long ApprovedCount();
        long NonApprovedCount();
        IList<TeacherWithTopScoreViewModel> GetTenTopScoreTeachers();
        IList<NewAddedTeacherViewModel> GetTenNewAddedTeachers();
    }
}