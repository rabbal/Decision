using System.Collections.Generic;

namespace Decision.ViewModel.Address
{
   public class AddressListViewModel
    {
       public IEnumerable<AddressViewModel> Addresses { get; set; }
       public AddressSearchRequest Request { get; set; }
    }
}
