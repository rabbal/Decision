using System.Collections.Generic;

namespace Decision.ViewModels.Address
{
   public class AddressListViewModel
    {
       public IEnumerable<AddressViewModel> Addresses { get; set; }
       public AddressListRequest Request { get; set; }
    }
}
