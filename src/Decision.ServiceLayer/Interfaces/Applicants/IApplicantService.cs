using System;
using System.Threading.Tasks;
using Decision.ViewModels.Applicant;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IApplicantService
    {
        Task<EditApplicantViewModel> GetForEditAsync(Guid id, string path);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditApplicantViewModel viewModel);
        void Create(AddApplicantViewModel viewModel);
        Task<ApplicantListViewModel> GetPagedListAsync(ApplicantListRequest request);
        Task<bool> IsInDb(Guid id);
        Task<bool> IsApplicantNationalCodeExist(string nationalCode, Guid? id);
        Task<bool> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id);
        Task FillEditViewMoel(EditApplicantViewModel viewModel, string path);
        Task FillAddViewMoel(AddApplicantViewModel viewModel, string path);
        Task<AddApplicantViewModel> GetForCreate(string path);
        Task<byte[]> GetApplicantDocument(Guid id, string type);
        Task<ApplicantDetailsViewModel> GetApplicantDetails(Guid id);
        Task<ApplicantViewModel> Approve(Guid id);
    }
}