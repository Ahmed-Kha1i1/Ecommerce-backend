using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.AddAddress
{
    public class AddAddressCommand : IRequest<Response<int?>>
    {
        public int CountryId { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
