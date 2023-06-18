using System.Text.Json.Serialization;

namespace API.Domain.User.Responses
{
    public class UserSignInResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        public UserSignInResponse() 
        {
            AccessToken = string.Empty;
            RefreshToken = string.Empty;
        }
    }
}
