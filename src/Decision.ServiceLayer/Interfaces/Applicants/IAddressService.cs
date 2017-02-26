using System;
using System.Threading.Tasks;
using Decision.ViewModels.Address;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IAddressService
    {
        Task<EditAddressViewModel> GetForEditAsync(Guid id,string path);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditAddressViewModel viewModel);
        Task<AddressViewModel> Create(AddAddressViewModel viewModel);
        Task<AddressListViewModel> GetAddressesAsync(AddressListRequest request);
        Task<bool> IsInDb(Guid id);
        void FillAddViewModel(AddAddressViewModel viewModel,string path);
        void FillEditViewModel(EditAddressViewModel viewModel,string path);
        AddAddressViewModel GetForCreate(Guid applicantId,string path);
        Task<AddressViewModel> GetAddressViewModel(Guid guid);
    }
}
