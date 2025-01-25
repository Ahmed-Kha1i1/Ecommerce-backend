using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Common.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Customers.Commands.EditCustomer
{
    public class UpdateCustomerCommand : IRequest<Response<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enGender Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}
