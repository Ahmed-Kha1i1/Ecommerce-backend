using Ecommerce.Application.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.OTP
{
    public class OTPRepository : IOTPRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;
        private readonly OtpSettings _otpSettings;
        private readonly Random _random;
        private readonly ILogger<OTPRepository> _logger;

        public OTPRepository(
            IMemoryCache memoryCache,
            IEmailService emailService,
            IOptions<OtpSettings> otpSettings,
            ILogger<OTPRepository> logger)
        {
            _memoryCache = memoryCache;
            _emailService = emailService;
            _otpSettings = otpSettings.Value;
            _random = new Random();
            _logger = logger;
        }

        public async Task SendOtp(string email)
        {
            try
            {
                var otp = GenerateOtp();

                var newEntry = new OtpEntry
                {
                    Otp = otp,
                    Timestamp = DateTime.UtcNow
                };

                _memoryCache.Set(GenerateOtpKey(email), newEntry, TimeSpan.FromMinutes(_otpSettings.OtpExpirationMinutes));

                var subject = "Your OTP Code";
                string htmlMessage = $@"
                    <html>
                    <body>
                        <p>Your OTP code is <strong>{otp}</strong>. It is valid for {_otpSettings.OtpExpirationMinutes} minutes.</p>
                        <p>If you did not request this code, please ignore this email.</p>
                        <br/>
                        <p>Best regards,<br/>Ecommerce</p>
                    </body>
                    </html>";

                await _emailService.SendEmailAsync(email, subject, htmlMessage, isHtmlMessage: true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP to {Email}", email);
                throw;
            }
        }

        public bool VerifyOtp(string email, string otp, string? ipAddress)
        {
            if (_memoryCache.TryGetValue(GenerateOtpKey(email), out OtpEntry? storedOtp) && storedOtp?.Otp == otp)
            {
                _memoryCache.Remove(GenerateOtpKey(email));
                _memoryCache.Set(GenerateVerifiedKey(email), ipAddress, TimeSpan.FromHours(_otpSettings.VerificationExpirationHours));
                return true;
            }

            _logger.LogWarning("Failed OTP verification attempt for {Email}", email);
            return false;
        }

        public bool IsVerified(string email, string? ipAddress)
        {
            if (_memoryCache.TryGetValue(GenerateVerifiedKey(email), out string? emailIpAddress))
            {
                if (emailIpAddress is null || emailIpAddress == ipAddress)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public bool HasOtp(string email, out int? remainingSeconds)
        {
            var cacheKey = GenerateOtpKey(email);

            if (_memoryCache.TryGetValue(cacheKey, out OtpEntry existingEntry))
            {
                var elapsedTime = DateTime.UtcNow - existingEntry.Timestamp;

                var allowedTimeSpan = TimeSpan.FromMinutes(_otpSettings.AllowSendOTPMinutes);

                var remainingTime = allowedTimeSpan - elapsedTime;

                if (remainingTime > TimeSpan.Zero)
                {
                    remainingSeconds = (int)Math.Ceiling(remainingTime.TotalSeconds);
                    return true;
                }
            }

            remainingSeconds = null;
            return false;
        }

        private string GenerateOtp()
        {
            return _random.Next(100000, 999999).ToString();
        }

        private string GenerateOtpKey(string email) => $"{_otpSettings.OtpPrefix}{email}";

        private string GenerateVerifiedKey(string email) => $"{_otpSettings.VerifiedPrefix}{email}";

        public Task RemoveVerificationAsync(string email, string? ipAddress)
        {
            _memoryCache.Remove(GenerateVerifiedKey(email));
            return Task.CompletedTask;
        }
    }
}