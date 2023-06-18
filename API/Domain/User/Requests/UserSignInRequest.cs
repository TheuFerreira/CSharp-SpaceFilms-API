namespace API.Domain.User.Requests
{
    public class UserSignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserSignInRequest()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
