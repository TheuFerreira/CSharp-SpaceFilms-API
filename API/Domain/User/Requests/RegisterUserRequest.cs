namespace API.Domain.User.Requests
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public RegisterUserRequest()
        {
            Name = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
    }
}
