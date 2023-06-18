using API.Domain.User.Models;

namespace API.Domain.User.Repositories
{
    public interface IUserRepository
    {
        int Create(UserModel user);
        UserModel? GetByEmailAndPassword(string email, string password);
        bool EmailExists(string email);
        UserModel? Get(int userId);
    }
}
