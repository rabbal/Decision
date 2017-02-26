using System;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface IServiceBase<in TKey, TEntityViewModel, TEditEntityViewModel, TCreateEntityViewModel, TListRequest, TListReponse> : IDisposable
    {
        Task<TEditEntityViewModel> GetForEditAsync(TKey id);
        Task DeleteAsync(TKey id);
        Task EditAsync(TEditEntityViewModel viewModel);
        Task<TEntityViewModel> Create(TCreateEntityViewModel viewModel);
        Task<TListRequest> FetchAllAsync(TListRequest request);
        Task<bool> Exists(Guid id);
        void FillCreateViewModel(TCreateEntityViewModel viewModel, string path);
        void FillEditViewModel(TEditEntityViewModel viewModel, string path);
        TCreateEntityViewModel GetForCreate(Guid applicantId, string path);
        Task<TEntityViewModel> GetAddressViewModel(TKey id);
    }
}
