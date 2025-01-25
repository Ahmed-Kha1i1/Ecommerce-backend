namespace Ecommerce.Application.Contracts
{
    public interface IOTPRepository
    {
        Task SendOtp(string email);
        bool VerifyOtp(string email, string otp, string? ipAddress);
        bool IsVerified(string email, string? ipAddress);
        bool HasOtp(string email, out int? remainingSeconds);
    }
}
