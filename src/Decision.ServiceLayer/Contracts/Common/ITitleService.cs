using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Title;

namespace Decision.ServiceLayer.Contracts.Common
{
    public interface ITitleService
    {
        /// <summary>
        /// واکشی تمام عنوان ها
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TitleListViewModel> GetPagedList(TitleSearchRequest request);
        /// <summary>
        /// واکشی عنوان برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditTitleViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف عنوان
        /// </summary>
        /// <param name="id">آی دی عنوان</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش عنوان
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش عنوان</param>
        /// <returns></returns>
        Task EditAsync(EditTitleViewModel viewModel);

        /// <summary>
        /// درج عنوان جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج عنوان</param>
        Task<TitleViewModel> Create(AddTitleViewModel viewModel);
        /// <summary>
        ///  چک کردن موجود بودن یک عنوان
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsByNameExist(string name, Guid? id, TitleType type, TitleCategory category);

        /// <summary>
        /// مشخص کردن اینکه آیا کاربر بتواند گروه را هم انتخاب کند برای این عنوان
        /// </summary>
        /// <param name="type">نوع عنوان انتخابی</param>
        /// <returns></returns>
        Task<bool> IsEnableCategorySelection(TitleType type);

        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        /// <summary>
        /// واکشی عنوان ها بر اساس نوع آنها برای لیست آبشاری
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(TitleType type,Guid? selectedId);

        Task<TitleViewModel> GetTitleViewModel(Guid guid);
    }
}