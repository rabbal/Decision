using System.ComponentModel;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class ApplicantListRequest : ListRequestBase
    {
        [DisplayName("نام")]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }
        [DisplayName("شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }
    }
}