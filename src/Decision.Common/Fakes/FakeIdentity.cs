using System;
using System.Security.Principal;

namespace NTierMvcFramework.Common.Fakes
{
    public class FakeIdentity : IIdentity
    {
        public FakeIdentity(string userName)
        {
            Name = userName;
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Name);

        public string Name { get; }
    }
}