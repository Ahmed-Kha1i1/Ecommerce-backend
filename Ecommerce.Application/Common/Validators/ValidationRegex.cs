
namespace Ecommerce.Application.Common.Validators
{
    public static class ValidationRegex
    {
        public const string NamePattern = @"^[A-Za-z0-9\-]+$";
        public const string PasswordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";
    }
}
