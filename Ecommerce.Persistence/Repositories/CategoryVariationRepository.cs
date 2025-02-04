﻿using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class CategoryVariationRepository : GenericRepository<CategoryVariation>, ICategoryVariationRepository
    {
        public CategoryVariationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
