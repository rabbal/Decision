using System.Collections.Generic;

namespace Decision.ViewModel.Role
{
   public class RoleListViewModel
    {
       public IEnumerable<RoleViewModel> Roles { get; set; }
       public RoleSearchRequest SearchRequest { get; set; }
    }
}
