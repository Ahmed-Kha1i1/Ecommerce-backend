using Ecommerce.Application;
using Ecommerce.Persistence;
using Serilog;

namespace Ecommerce.API
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        public Startup(IConfigurationRoot configuration)
        {

            _configuration = configuration;
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            ConfigureLogging(builder);

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

        public void ConfigureServices(IServiceCollection Services)
        {
            // Add services to the container.
            Services.AddApplicationServices(_configuration)
                    .AddPersistenceServices(_configuration);

            Services.AddProblemDetails();
            Services.AddControllers();
            Services.AddOpenApi();
        }

        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
