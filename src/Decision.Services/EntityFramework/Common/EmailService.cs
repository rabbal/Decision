using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EntityFramework.Common
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}
