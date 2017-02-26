using System;
using System.Threading.Tasks;
using Decision.ViewModels.EducationalExperience;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IEducationalExperienceService
    {
        Task<EditEducationalExperienceViewModel> GetForEditAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditEducationalExperienceViewModel viewModel);
        Task<EducationalExperienceViewModel> Create(AddEducationalExperienceViewModel viewModel);
        Task<EducationalExperienceListViewModel> GetPagedListAsync(EducationalExperienceSearchRequest request);
        Task<bool> IsInDb(Guid id);
        Task<EducationalExperienceViewModel> GetEducationalExperienceViewModel(Guid guid);
    }
}