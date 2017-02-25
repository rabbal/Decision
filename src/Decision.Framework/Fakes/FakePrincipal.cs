using System.Linq;
using System.Security.Principal;

namespace Decision.Framework.Fakes
{
    public class FakePrincipal : IPrincipal
    {
        private readonly string[] _roles;

        public FakePrincipal(IIdentity identity, string[] roles)
        {
            Identity = identity;
            _roles = roles;
        }


        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return _roles != null && _roles.Contains(role);
        }
    }
}