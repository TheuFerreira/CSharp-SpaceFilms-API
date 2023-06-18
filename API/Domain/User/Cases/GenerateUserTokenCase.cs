using API.Domain.User.Responses;
using API.Domain.User.Services;

namespace API.Domain.User.Cases
{
    public class GenerateUserTokenCase
    {
        private readonly ITokenService tokenService;
        private readonly Token token;

        public GenerateUserTokenCase(ITokenService tokenService, Token token)
        {
            this.tokenService = tokenService;
            this.token = token;
        }

        public UserSignInResponse Execute(int userId)
        {
            DateTime expiresAt = DateTime.UtcNow.AddMinutes(token.TimeToAccessExpires);
            DateTime refreshExpiresAt = DateTime.UtcNow.AddMinutes(token.TimeToRefreshExpires);

            string accessToken = tokenService.Generate(userId, expiresAt);
            string refreshToken = tokenService.Generate(userId, refreshExpiresAt);

            return new UserSignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
            };
        }
    }
}
