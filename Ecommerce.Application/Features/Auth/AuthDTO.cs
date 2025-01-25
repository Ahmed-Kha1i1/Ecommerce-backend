using System.Text.Json.Serialization;

namespace Ecommerce.Application.Features.Auth
{
    public class AuthDTO
    {
        public int CustomerId { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
