using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.TrainingCourse;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس دوره های کارآموزی
    /// </summary>
    public interface ITrainingCourseService
    {
        /// <summary>
        /// واکشی آی دی مرکز کارآموزی 
        /// </summary>
        /// <param name="id">آی دی دوره</param>
        /// <returns></returns>
        Task<TrainingCourse> Get(Guid id);
        /// <summary>
        /// لیست آدرس های مرکز کارآموزی
        /// </summary>
        /// <param name="centerId">آی دی مرکز کار آموزی</param>
        /// <returns></returns>
        Task<IEnumerable<TrainingCourseViewModel>> GetByTrainingCenterIdAsync(Guid centerId);
        /// <summary>
        /// واکشی دوره آموزشی برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditTrainingCourseViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف دوره آموزشی
        /// </summary>
        /// <param name="id">آی دی دوره آموزشی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش دوره آموزشی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش دوره آموزشی</param>
        /// <returns></returns>
        Task EditAsync(EditTrainingCourseViewModel viewModel);

        /// <summary>
        /// درج دوره آموزشی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج دوره آموزشی</param>
        Task<TrainingCourseViewModel> Create(AddTrainingCourseViewModel viewModel);

        /// <summary>
        /// نمایش لیست دوره آموزشی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<TrainingCourseListViewModel> GetPagedListAsync(TrainingCourseSearchRequest request);

        /// <summary>
        /// نمایش لیست دوره آموزشی ها به صورت آبشاری 
        /// </summary>
        /// <param name="trainingCenterId">آی دی مرکز کارآموزی مورد نظر</param>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAsSelectListByTrainingCenterIdAsync(Guid trainingCenterId, Guid? selectedId);
        /// <summary>
        /// نشان دهنده این است که آیا دوره ای با این کد قبلا ثبت شده است؟
        /// </summary>
        /// <param name="code">کد دوره</param>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsExistCourseCode(string code, Guid? id,Guid centerId);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        /// <summary>
        /// واکشی دوره کارآموزی متقاضی به همراه مرکزی که در آن دوره برگزار شده است
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<TrainingCourse> GetTrainingCourseOfApplicant(Guid courseId);

        Task<TrainingCourseViewModel> GetTrainingCourseViewModel(Guid id);
    }
}
