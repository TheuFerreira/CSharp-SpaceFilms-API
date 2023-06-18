namespace API.Domain.User.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EmailChecked { get; set; }

        public UserModel()
        {
            Name = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
    }
}
