using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Decision.Framework.IdentityToolkit.Validators
{
    public class NullPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {
            return Task.FromResult(IdentityResult.Success);
        }

    }
}