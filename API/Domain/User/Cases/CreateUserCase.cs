using API.Domain.User.Errors;
using API.Domain.User.Models;
using API.Domain.User.Repositories;
using API.Domain.User.Requests;

namespace API.Domain.User.Cases
{
    public class CreateUserCase
    {
        private readonly IUserRepository userRepository;

        public CreateUserCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int Execute(RegisterUserRequest request)
        {
            UserModel user = new()
            {
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
            };

            bool emailExists = userRepository.EmailExists(request.Email);
            if (emailExists)
                throw new UserEmailExistsException();

            user.UserId = userRepository.Create(user);
            return user.UserId;
        }
    }
}
