using System;
using System.Threading.Tasks;
using Decision.ViewModels.EducationalBackground;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IEducationalBackgroundService
    {
        Task<EditEducationalBackgroundViewModel> GetForEditAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditEducationalBackgroundViewModel viewModel);
        Task<EducationalBackgroundViewModel> Create(AddEducationalBackgroundViewModel viewModel);
        Task<EducationalBackgroundListViewModel> GetPagedListAsync(EducationalBackgroundSearchRequest request);
        Task<bool> IsInDb(Guid guid);
        
    }
}