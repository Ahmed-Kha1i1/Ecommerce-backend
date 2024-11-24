﻿using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence
{
    public static class PersistenceRegistraction
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryVariationRepository, CategoryVariationRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariationRepository, ProductVariationRepository>();
            services.AddScoped<IVariationOptionRepository, VariationOptionRepository>();
            services.AddScoped<IVariationRepository, VariationRepository>();

            services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}
