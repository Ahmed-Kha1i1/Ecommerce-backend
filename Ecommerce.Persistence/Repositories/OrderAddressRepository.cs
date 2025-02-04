﻿using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class OrderAddressRepository : GenericRepository<OrderAddress>, IOrderAddressRepository
    {
        public OrderAddressRepository(AppDbContext context) : base(context)
        {
        }
    }
}
