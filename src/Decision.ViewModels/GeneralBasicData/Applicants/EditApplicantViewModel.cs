using Decision.DomainClasses.Applicants;
using Decision.Framework.Domain.Entities;
using Decision.Framework.MapperToolkit;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class EditApplicantViewModel : CreateApplicantViewModel, IEntity, IMapFrom<Applicant>
    {
        #region Properties
        public long Id { get; set; }
        public byte[] RowVersion { get; set; }

        #endregion
    }
}