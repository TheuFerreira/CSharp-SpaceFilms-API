using API.Domain.User.Errors;
using API.Domain.User.Models;
using API.Domain.User.Repositories;

namespace API.Domain.User.Cases
{
    public class GetUserByIdCase
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel Execute(int userId)
        {
            UserModel model = userRepository.Get(userId) ?? throw new UserNotFoundException();
            return model;
        }
    }
}
