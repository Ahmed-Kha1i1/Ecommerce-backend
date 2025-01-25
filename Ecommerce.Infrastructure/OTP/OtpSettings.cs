namespace Ecommerce.Infrastructure.OTP
{
    public class OtpSettings
    {
        public int OtpExpirationMinutes { get; set; }
        public int VerificationExpirationHours { get; set; }
        public string OtpPrefix { get; set; }
        public string VerifiedPrefix { get; set; }
        public int AllowSendOTPMinutes { get; set; }
    }
}
