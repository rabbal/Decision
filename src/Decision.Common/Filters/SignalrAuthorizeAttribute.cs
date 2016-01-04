using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Common.Filters
{
    public class SignalrAuthorizeAttribute : Microsoft.AspNet.SignalR.AuthorizeAttribute
    {
        public SignalrAuthorizeAttribute(params string[] permissions)
            : base()
        {
            Roles = string.Join(",", permissions);
        }

    }
}
