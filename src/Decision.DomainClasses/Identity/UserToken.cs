using System;
using Decision.Framework.Domain;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Identity
{
    public class UserToken : Entity
    {
        #region Properties

        public string AccessTokenHash { get; set; }
        public DateTimeOffset AccessTokenExpireDateTime { get; set; }
        public string RefreshTokenIdHash { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset RefreshTokenExpiresUtc { get; set; }
        public string RefreshToken { get; set; }

        #endregion

        #region NavigationProperties

        public long UserId { get; set; }
        public User User { get; set; }

        #endregion
    }
}