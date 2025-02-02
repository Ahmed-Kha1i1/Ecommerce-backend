using Ecommerce.API.Base;
using Ecommerce.API.Configurations;
using Ecommerce.Application;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Infrastructure;
using Ecommerce.Persistence;
using Ecommerce.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

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
            builder.Configuration.AddJsonFile("configsettings.json");

            builder.Services.AddAuthorization();
            ConfigureLogging(builder);

            builder.Services.AddMemoryCache();
            builder.Host.UseSerilog();
        }

        #region Configure Logging
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
        #endregion

        #region Configure Identity
        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };

                options.Cookie.Name = IdentityConstants.ApplicationScheme;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
            });
        }
        #endregion

        #region Configure Authentication
        private void ConfigureAuthentication(IServiceCollection services)
        {
            var googleAuthSettings = _configuration.GetSection("Authorization:Google").Get<GoogleOAuthSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            }).AddGoogle(options =>
            {
                options.ClientId = googleAuthSettings.ClientId;
                options.ClientSecret = googleAuthSettings.ClientSecret;
                options.Events.OnTicketReceived = async context =>
                {
                    context.HandleResponse();

                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                    var customerRepository = context.HttpContext.RequestServices.GetRequiredService<ICustomerRepository>();

                    signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;
                    var email = context.Principal.FindFirstValue(ClaimTypes.Email);

                    var user = await userRepository.FindByEmailIncludeDeletedAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = UsernameGenerator.GenerateUniqueUsername(email),
                            Email = email,
                            FirstName = context.Principal.FindFirstValue(ClaimTypes.GivenName),
                            LastName = context.Principal.FindFirstValue(ClaimTypes.Surname),
                            CreatedDate = DateTime.UtcNow,
                            EmailConfirmed = true,
                            LockoutEnabled = false
                        };

                        var result = await userManager.CreateAsync(user);
                        var roleResult = await userManager.AddToRoleAsync(user, "Customer");

                        var customer = new Customer
                        {
                            Id = user.Id
                        };

                        await customerRepository.AddCustomerToExistsUser(customer.Id);
                        if (result.Succeeded)
                        {
                            // Sign in the newly created user
                            await signInManager.SignInAsync(user, isPersistent: true);
                        }
                    }
                    else
                    {
                        if (user.IsDeleted)
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return;
                        }

                        await signInManager.SignInAsync(user, isPersistent: true);
                    }
                    context.Response.Redirect(googleAuthSettings.RedirectUri);
                };
            });
        }
        #endregion

        #region Configure Services
        public void ConfigureServices(IServiceCollection services)
        {
            var allowedOrigins = _configuration.GetSection("AllowedOrigins").Get<string[]>();

            // Add services to the container.
            services.AddApplicationServices(_configuration)
                    .AddPersistenceServices(_configuration)
                    .AddInfrastructureServices(_configuration);

            ConfigureIdentity(services);
            ConfigureAuthentication(services);

            services.AddProblemDetails();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            });

            services.AddOpenApi();
            services.AddHttpContextAccessor();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowsOrigins, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("Location");
                });
            });
        }
        #endregion

        #region Configure Application
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
        #endregion
    }
}