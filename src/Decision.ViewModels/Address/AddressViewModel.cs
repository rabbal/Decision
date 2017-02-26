using System.ComponentModel;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Address
{
    public class AddressViewModel : BaseViewModel
    {
        #region Properties
        public long Id { get; set; }
        
        [DisplayName("استان")]
        public string State { get; set; }

        [DisplayName("شهر")]
        public string City { get; set; }

        [DisplayName("شماره همراه")]
        public string CellPhone { get; set; }
        
        [DisplayName("نشانی")]
        public string Location { get; set; }
        
        [DisplayName("تلفن ثابت")]
        public string PhoneNumber { get; set; }
        public AddressType Type { get; set; }
        public long ApplicantId { get; set; }

        #endregion
    }
}