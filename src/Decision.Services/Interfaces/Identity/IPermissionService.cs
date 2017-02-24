using System.Collections.Generic;
using System.Xml.Linq;

namespace Decision.ServiceLayer.Interfaces.Identity
{
    public interface IPermissionService
    {
        XElement GetPermissionsAsXml(params string[] permissionNames);
        IList<string> GetUserPermissionsAsList(XElement permissionsAsXml);
        IList<string> GetUserPermissionsAsList(IList<XElement> permissionsAsXmls);
        IEnumerable<string> GetDescriptions(XElement permissionsAsXml);

    }
}
