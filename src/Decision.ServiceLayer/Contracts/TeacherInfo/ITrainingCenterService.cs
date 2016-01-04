using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.ViewModel.TrainingCenter;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس مرکز کارآموزی
    /// </summary>
    public interface  ITrainingCenterService
    {
        /// <summary>
        /// واکشی نام استان و شهر مرکز
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Tuple<string, string>> GetCityAndState(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(string city,Guid? selected);
        /// <summary>
        /// چک کردن موجود بودن نام مرکز کار آموزی
        /// </summary>
        /// <param name="name">نام مرکز</param>
        /// <param name="id">آی دی مرکز</param>
        /// <returns></returns>
        Task<bool> IsNameExistAsync(string name, Guid? id,string city);

        /// <summary>
        /// واکشی مرکز کارآموزی برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="path">آدرس مخزن شهر ها</param>
        /// <returns></returns>
        Task<EditTrainingCenterViewModel> GetForEditAsync(Guid id, string path);
        /// <summary>
        /// حذف مرکز کارآموزی
        /// </summary>
        /// <param name="id">آی دی مرکز کارآموزی</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش مرکز کارآموزی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش مرکز کارآموزی</param>
        /// <returns></returns>
        Task EditAsync(EditTrainingCenterViewModel viewModel);
        /// <summary>
        /// درج مرکز کارآموزی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج مرکز کارآموزی</param>
        Task<TrainingCenterViewModel> Create(AddTrainingCenterViewModel viewModel);
        /// <summary>
        /// نمایش لیست مرکز کارآموزی ها با امکان جستجو و مرتب سازی
        /// </summary>
        /// <param name="request">اطلاعات مرتب سازی و جستجو</param>
        /// <returns></returns>
        Task<TrainingCenterListViewModel> GetPagedListAsync(TrainingCenterSearchRequest request);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        /// <summary>
        /// مقدار دهی لیست های مرتبط
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task FillEditViewMode(EditTrainingCenterViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست های مرتبط
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task FillAddViewMolde(AddTrainingCenterViewModel viewModel,string path);
        /// <summary>
        /// مقدار دهی لیست های مرتبط
        /// </summary>
        /// <returns></returns>
        Task<AddTrainingCenterViewModel> GetForCreate(string path);

        Task<TrainingCenterViewModel> GetCenterViewModel(Guid id);
    }
}
