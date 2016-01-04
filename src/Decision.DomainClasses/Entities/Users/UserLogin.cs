using System;
using Decision.Utility;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// نشان دهنده لیست مهیا کننده هایی که کاربر از آنها باری ورود استفاده میکند
    /// <remarks>از قبیل فیسبوک/گوگل</remarks>
    /// </summary>
    public class UserLogin : IdentityUserLogin<Guid>
    {
    }
}
