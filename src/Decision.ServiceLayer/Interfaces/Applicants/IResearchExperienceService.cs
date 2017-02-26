using System;
using System.Threading.Tasks;
using Decision.ViewModels.ResearchExperience;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IResearchExperienceService
    {
        Task<EditResearchExperienceViewModel> GetForEditAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditResearchExperienceViewModel viewModel);
        Task<ResearchExperienceViewModel> Create(AddResearchExperienceViewModel viewModel);
        Task<ResearchExperienceListViewModel> GetPagedListAsync(ResearchExperienceSearchRequest request);
        Task<bool> IsInDb(Guid id);
        Task<ResearchExperienceViewModel> GetResearchExperienceViewModel(Guid guid);
    }
}