using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.ApplicantInfo
{
  
    public class Presenter : BaseEntity
    {
        #region Properties
        
        public string FullName { get; set; }
        
        public string RelationType { get; set; }
    
        public string PhoneNumber { get; set; }
  
        public string CellPhoneNumber { get; set; }

        public string DurationOfRelation { get; set; }

        public string Job { get; set; }

        public PresenterType Type { get; set; }
        #endregion

        #region NavigationProperties
      
        
        public Applicant Applicant { get; set; }
     
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
