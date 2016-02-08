using System;
using System.Threading.Tasks;
using Decision.ViewModel.Address;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    public interface IAddressService
    {
        /// <summary>
        /// واکشی آدرس برای ویرایش
        /// </summary>
        /// <param name="id">آی دی آدرس</param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<EditAddressViewModel> GetForEditAsync(Guid id,string path);

        /// <summary>
        /// حذف آدرس
        /// </summary>
        /// <param name="id">آی دی آدرس</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// ویرایش آدرس
        /// </summary>
        /// <param name="viewModel">ویومدل آدرس برای ویرایش</param>
        /// <returns></returns>
        Task EditAsync(EditAddressViewModel viewModel);
        /// <summary>
        /// درج آدرس جدید
        /// </summary>
        /// <param name="viewModel">ویومدل درج آدرس</param>
        Task<AddressViewModel> Create(AddAddressViewModel viewModel);
        /// <summary>
        /// دریافت لیست آدرس های یک متقاضی
        /// </summary>
        Task<AddressListViewModel> GetAddressesAsync(AddressSearchRequest request);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);

        void FillAddViewModel(AddAddressViewModel viewModel,string path);
        void FillEditViewModel(EditAddressViewModel viewModel,string path);
        AddAddressViewModel GetForCreate(Guid applicantId,string path);

        Task<AddressViewModel> GetAddressViewModel(Guid guid);
    }
}
