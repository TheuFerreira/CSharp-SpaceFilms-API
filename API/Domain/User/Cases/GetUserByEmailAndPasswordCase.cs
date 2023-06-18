using API.Domain.User.Errors;
using API.Domain.User.Models;
using API.Domain.User.Repositories;

namespace API.Domain.User.Cases
{
    public class GetUserByEmailAndPasswordCase
    {
        private readonly IUserRepository userRepository;

        public GetUserByEmailAndPasswordCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Execute(string email, string password)
        {
            UserModel user = userRepository.GetByEmailAndPassword(email, password) ?? throw new UserNotFoundException();
            if (!user.EmailChecked) throw new UserEmailNotCheckedException();

            return user;
        }
    }
}
