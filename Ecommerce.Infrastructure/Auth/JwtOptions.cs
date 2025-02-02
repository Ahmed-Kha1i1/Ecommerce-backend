namespace Ecommerce.Infrastructure.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public List<string> Audiences { get; set; }
        public short Lifetime { get; set; }
        public string SigningKey { get; set; }
    }

}
