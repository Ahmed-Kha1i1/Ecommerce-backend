﻿using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
    }
}