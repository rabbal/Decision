using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Decision.Framework.IdentityToolkit.Validators
{
    public class NullUserValidator<TUser, TKey> : IIdentityValidator<TUser>
        where TUser : class, IUser<long>
        where TKey : IEquatable<long>
    {
        public Task<IdentityResult> ValidateAsync(TUser item)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }

}
