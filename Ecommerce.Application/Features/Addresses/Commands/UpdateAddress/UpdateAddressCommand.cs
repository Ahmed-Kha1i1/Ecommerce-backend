using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<Response<bool>>
    {
        public int CountryId { get; set; }
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
