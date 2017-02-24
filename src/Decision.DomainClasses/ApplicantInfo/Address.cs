using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.ApplicantInfo
{
    public class Address : BaseEntity
    {
        #region Properties
        public string CellPhone { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public AddressType Type { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        #endregion

        #region NavigationProperties
        public Applicant Applicant { get; set; }
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
