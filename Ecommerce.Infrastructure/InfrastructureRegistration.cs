using Ecommerce.Application.Contracts;
using Ecommerce.Doman.Entities;
using Ecommerce.Infrastructure.Auth;
using Ecommerce.Infrastructure.Email;
using Ecommerce.Infrastructure.OTP;
using Ecommerce.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.Configure<OtpSettings>(configuration.GetSection("OtpSettings"));
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            services.AddTransient<IEmailSender<User>, IdentityEmailSender>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthRespository, AuthRespository>();
            services.AddTransient<IOTPRepository, OTPRepository>();

            services.AddMemoryCache();
            return services;
        }


    }
}
