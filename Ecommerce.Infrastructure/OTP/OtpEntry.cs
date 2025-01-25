namespace Ecommerce.Infrastructure.OTP
{
    public record OtpEntry
    {
        public string Otp { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
