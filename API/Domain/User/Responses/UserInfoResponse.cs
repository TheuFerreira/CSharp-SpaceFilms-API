namespace API.Domain.User.Responses
{
    public class UserInfoResponse
    {
        public string Name { get; set; }

        public UserInfoResponse()
        {
            Name = string.Empty;
        }
    }
}
