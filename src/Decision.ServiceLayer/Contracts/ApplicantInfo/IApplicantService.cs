using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Home;
using Decision.ViewModel.Applicant;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس متقاضی
    /// </summary>
    public interface IApplicantService
    {
        /// <summary>
        /// واکشی متقاضی برای ویرایش
        /// </summary>
        /// <param name="id">آی دی متقاضی</param>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task<EditApplicantViewModel> GetForEditAsync(Guid id, string path);
        /// <summary>
        /// حذف متقاضی
        /// </summary>
        /// <param name="id">آی دی متقاضی</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش متقاضی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش متقاضی</param>
        /// <returns></returns>
        Task EditAsync(EditApplicantViewModel viewModel);
        /// <summary>
        /// درج متقاضی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج متقاضی</param>
        void Create(AddApplicantViewModel viewModel);
        /// <summary>
        /// نمایش لیست متقاضی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<ApplicantListViewModel> GetPagedListAsync(ApplicantSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        /// <summary>
        /// چک کردن موجود بودن کدملی متقاضی
        /// </summary>
        /// <param name="nationalCode">کدملی</param>
        /// <param name="id">آی دی <c>میتواند نال باشد</c></param>
        /// <returns></returns>
        Task<bool> IsApplicantNationalCodeExist(string nationalCode, Guid? id);

        /// <summary>
        /// چک کردن موجود بودن شماره شناسنامه متقاضی
        /// </summary>
        /// <param name="birthCertificateNumber">شماره شناسنامه</param>
        /// <param name="id">آی دی <c>میتواند نال باشد</c></param>
        /// <returns></returns>
        Task<bool> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id);

        /// <summary>
        /// مقدار دهی لیست های مربوط به ویو مدل ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task FillEditViewMoel(EditApplicantViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست های مربوط به ویو مدل درج
        /// </summary>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task FillAddViewMoel(AddApplicantViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست ها مربوطه
        /// </summary>
        /// <param name="path">مسیر مخزن شهرها</param>
        /// <returns></returns>
        Task<AddApplicantViewModel> GetForCreate(string path);
        Task<byte[]> GetApplicantDocument(Guid id,string type);
        Task<ApplicantDetailsViewModel> GetApplicantDetails(Guid id);
        Task<ApplicantViewModel> Approve(Guid id);

    }
}