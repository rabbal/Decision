using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.Framework.Domain.Services;
using Decision.ViewModels.GeneralBasicData.Applicants;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IApplicantService :
        IServiceBase
            <long, ApplicantViewModel, CreateApplicantViewModel, EditApplicantViewModel, ApplicantListRequest,
                ApplicantListViewModel>
    {
       bool CheckFirstNameExist(string firstName, long? id);
        bool CheckLastNameExist(string lastName, long? id);
        bool CheckEmailAddressExist(string emailAddress, long? id);
        bool CheckNationalCodeExist(string nationalCode, long? id);
        bool CheckBirthCertificateNumberExist(string birthCertificateNumber, long? id);
        
        Task<bool> CheckFirstNameExistAsync(string firstName, long? id);
        Task<bool> CheckLastNameExistAsync(string lastName, long? id);
        Task<bool> CheckEmailAddressExistAsync(string emailAddress, long? id);
        Task<bool> CheckNationalCodeExistAsync(string nationalCode, long? id);
        Task<bool> CheckBirthCertificateNumberExistAsync(string birthCertificateNumber, long? id);
    }
}