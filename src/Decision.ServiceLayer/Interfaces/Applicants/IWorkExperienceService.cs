using System;
using System.Threading.Tasks;
using Decision.ViewModels.WorkExperience;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IWorkExperienceService
    {
        Task<EditWorkExperienceViewModel> GetForEditAsync(Guid id, string path);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditWorkExperienceViewModel viewModel);
        Task<WorkExperienceViewModel> Create(AddWorkExperienceViewModel viewModel);
        Task<WorkExperienceListViewModel> GetPagedListAsync(WorkExperienceSearchRequest request);
        Task<bool> IsInDb(Guid id);
        Task<AddWorkExperienceViewModel> GetForCreate(Guid ApplicantId, string path);
        Task FillAddViewModel(AddWorkExperienceViewModel viewModel, string path);
        Task FillEditViewModel(EditWorkExperienceViewModel viewModel, string path);
        Task<WorkExperienceViewModel> GetWorkExperienceViewModel(Guid guid);
    }
}