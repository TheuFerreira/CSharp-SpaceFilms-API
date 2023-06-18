namespace API.Domain.User.Services
{
    public interface ITokenService
    {
        string Generate(int userId, DateTime expiresAt);
    }
}
