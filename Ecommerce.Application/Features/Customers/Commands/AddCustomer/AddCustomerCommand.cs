using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Common.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Customers.Commands.AddCustomer
{
    public class AddCustomerCommand : IRequest<Response<int?>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public enGender? Gender { get; set; }
    }
}
