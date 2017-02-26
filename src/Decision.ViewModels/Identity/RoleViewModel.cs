using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Decision.ViewModels.Identity
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            PermissionsList=new List<string>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  bool IsBanned { get; set; }
        public bool IsSystemRole { get; set; }
        public IEnumerable<string> PermissionsList { get; set; }
        public  string Permissions { get; set; }
        public XElement XmlPermission
        {
            get { return XElement.Parse(Permissions); }
        }

    }
}
