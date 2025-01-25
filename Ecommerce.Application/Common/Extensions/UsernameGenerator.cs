namespace Ecommerce.Application.Common.Extensions
{
    public static class UsernameGenerator
    {
        public static string GenerateUniqueUsername(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 8);
            string username = $"{email.Split('@')[0]}_{uniqueSuffix}";
            return username;
        }
    }
}
