using Ecommerce.API.Base;
using Ecommerce.API.Configurations;
using Ecommerce.Application;
using Ecommerce.Doman.Entities;
using Ecommerce.Infrastructure;
using Ecommerce.Persistence;
using Ecommerce.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Ecommerce.API
{
    public class Startup
    {
        private string AllowsOrigins = "AllowSpicificOrigin";
        private readonly IConfigurationRoot _configuration;
        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            ConfigureLogging(builder);

            builder.Services.AddMemoryCache();
            builder.Host.UseSerilog();
        }

        private void ConfigureLogging(WebApplicationBuilder builder)
        {
            var logConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.File("logs/MyAppLog.txt")
                .Enrich.FromLogContext();

            if (builder.Environment.IsDevelopment())
            {
                logConfiguration.WriteTo.Console();
            }

            Log.Logger = logConfiguration.CreateLogger();
        }


        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var facebookAuthSettings = _configuration.GetSection("Authorization:Facebook").Get<FacebookOptions>();
            var googleAuthSettings = _configuration.GetSection("Authorization:Google").Get<GoogleOptions>();

            services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = googleAuthSettings.ClientId;
                options.ClientSecret = googleAuthSettings.ClientSecret;
            }).AddFacebook(options =>
            {
                options.AppId = facebookAuthSettings.AppId;
                options.AppSecret = facebookAuthSettings.AppSecret;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.Cookie.Name = IdentityConstants.ApplicationScheme;
                options.Cookie.Domain = "localhost";
            });
        }

        public void ConfigureServices(IServiceCollection Services)
        {
            // Add services to the container.
            Services.AddApplicationServices(_configuration)
                    .AddPersistenceServices(_configuration)
                    .AddInfrastructureServices(_configuration);

            ConfigureAuthentication(Services);
            ConfigureIdentity(Services);

            Services.AddProblemDetails();
            Services.AddControllers();
            Services.AddOpenApi();
            Services.AddHttpContextAccessor();

            Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All;
            });

            Services.AddCors(options =>
            {
                options.AddPolicy(AllowsOrigins, policy =>
                {
                    policy.WithOrigins("https://localhost:5173").AllowCredentials().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Location");
                });
            });
        }

        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseForwardedHeaders();

            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors(AllowsOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.CustomMapIdentityApi<User>();
        }
    }
}