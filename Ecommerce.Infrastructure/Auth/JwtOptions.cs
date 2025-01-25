namespace Ecommerce.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public short Lifetime { get; set; }
        public string SigningKey { get; set; }
    }

}
