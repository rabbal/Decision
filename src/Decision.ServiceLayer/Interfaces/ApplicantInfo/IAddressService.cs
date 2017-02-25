using System;
using System.Threading.Tasks;
using Decision.ViewModel.Address;

namespace Decision.ServiceLayer.Interfaces.ApplicantInfo
{
    public interface IAddressService
    {
        Task<EditAddressViewModel> GetForEditAsync(Guid id,string path);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditAddressViewModel viewModel);
        Task<AddressViewModel> Create(AddAddressViewModel viewModel);
        Task<AddressListViewModel> GetAddressesAsync(AddressSearchRequest request);
        Task<bool> IsInDb(Guid id);
        void FillAddViewModel(AddAddressViewModel viewModel,string path);
        void FillEditViewModel(EditAddressViewModel viewModel,string path);
        AddAddressViewModel GetForCreate(Guid applicantId,string path);
        Task<AddressViewModel> GetAddressViewModel(Guid guid);
    }
}
