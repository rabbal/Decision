using System;
using System.Threading.Tasks;
using Decision.ViewModel.EducationalExperience;

namespace Decision.ServiceLayer.Interfaces.ApplicantInfo
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