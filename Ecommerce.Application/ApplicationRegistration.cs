using Ecommerce.Application.Common.Validators;
using Ecommerce.Application.Exceptions.Handlers;
using Ecommerce.Application.Features.Orders;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Ecommerce.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            //Confiure Auto Mapper 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Configure Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.Configure<OrderSettings>(configuration.GetSection("OrderSettings"));
            // Configure Fluent Validation
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableBuiltInModelValidation = true;
                config.EnableBodyBindingSourceAutomaticValidation = true;
                config.EnableFormBindingSourceAutomaticValidation = true;
                config.EnableQueryBindingSourceAutomaticValidation = true;
                config.EnablePathBindingSourceAutomaticValidation = true;
                config.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
            });

            return services;
        }
    }
}
