namespace API.Domain.User.Requests
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }

        public ResetPasswordRequest()
        {
            Email = string.Empty;
        }
    }
}
