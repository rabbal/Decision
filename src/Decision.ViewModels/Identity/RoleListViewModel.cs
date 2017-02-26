using System.Collections.Generic;

namespace Decision.ViewModels.Identity
{
   public class RoleListViewModel
    {
       public IEnumerable<RoleViewModel> Roles { get; set; }
       public RoleSearchRequest SearchRequest { get; set; }
    }
}
