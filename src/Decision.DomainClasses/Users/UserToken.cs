using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Users
{
    public class UserToken : IHaveGuidKey
    {
        #region Properties

        public Guid Id { get; set; }
        public string AccessTokenHash { get; set; }
        public DateTime AccessTokenExpireOn { get; set; }
        public string RefreshTokenIdHash { get; set; }
        public string Subject { get; set; }
        public DateTime RefreshTokenExpiresUtc { get; set; }
        public string RefreshToken { get; set; }
        #endregion

        #region NavigationProperties
        public long UserId   { get; set; }
        public User User { get; set; }
        #endregion
    }
}