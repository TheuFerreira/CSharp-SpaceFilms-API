using API.Domain.User.Errors;
using API.Domain.User.Repositories;

namespace API.Domain.User.Cases
{
    public class ResetUserPasswordCase
    {
        private readonly IUserRepository userRepository;

        public ResetUserPasswordCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Execute(string email) 
        {
            bool emailExists = userRepository.EmailExists(email);
            if (!emailExists)
                throw new UserEmailNotExistsException();
        }
    }
}
