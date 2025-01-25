using AutoMapper;
using Ecommerce.Application.Features.Customers.Commands.EditCustomer;
using Ecommerce.Application.Features.Customers.Queries.CustomerDetailsQuery;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomerDetailsQueryResponse>();
            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
