using System.Collections.Generic;

namespace Decision.ServiceLayer.EntityFramework.Security
{
    public  class PermissionRecord
    {
        public string RoleName { get; set; }
        public IEnumerable<PermissionModel> Permissions { get; set; }
    }
}
