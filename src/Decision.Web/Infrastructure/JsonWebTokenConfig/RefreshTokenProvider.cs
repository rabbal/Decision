using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Infrastructure;
using Decision.DomainClasses.Users;
using Decision.Services.Interfaces.Security;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    /// <summary>
    ///     With the refresh token the user does not need to login again and
    ///     they can use refresh token to request a new authorization token.
    /// </summary>
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IAppJwtConfiguration _configuration;
        private readonly Func<ISecurityService> _securityService;
        private readonly Func<ITokenStoreService> _tokenStoreService;

        public RefreshTokenProvider(
            IAppJwtConfiguration configuration,
            Func<ITokenStoreService> tokenStoreService,
            Func<ISecurityService> securityService)
        {
            _configuration = configuration;
            _tokenStoreService = tokenStoreService;
            _securityService = securityService;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context).RunSynchronously();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            var now = DateTime.UtcNow;
            var ownerUserId = context.Ticket.Identity.FindFirstValue(ClaimTypes.UserData);
            var token = new UserToken
            {
                UserId = long.Parse(ownerUserId),
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = _securityService().GetSha256Hash(refreshTokenId),
                Subject = context.Ticket.Identity.Name,
                RefreshTokenExpiresUtc = now.AddMinutes(Convert.ToDouble(_configuration.RefreshTokenExpirationMinutes)),
                AccessTokenExpireOn = now.AddMinutes(Convert.ToDouble(_configuration.ExpirationMinutes))
            };

            context.Ticket.Properties.IssuedUtc = now;
            context.Ticket.Properties.ExpiresUtc = token.RefreshTokenExpiresUtc;

            token.RefreshToken = context.SerializeTicket();

            await _tokenStoreService().CreateUserTokenAsync(token);
            await _tokenStoreService().DeleteExpiredTokensAsync();

            context.SetToken(refreshTokenId);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context).RunSynchronously();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var hashedTokenId = _securityService().GetSha256Hash(context.Token);
            var refreshToken = await _tokenStoreService().FindTokenAsync(hashedTokenId);

            if (refreshToken == null) return;

            context.DeserializeTicket(refreshToken.RefreshToken);
            await _tokenStoreService().DeleteTokenAsync(hashedTokenId);
        }
    }
}