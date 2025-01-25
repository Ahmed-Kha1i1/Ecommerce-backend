using Ecommerce.Application.Features.Countries.Queries;

namespace Ecommerce.Application.Features.Orders.Queries
{
    public class OrderAddressDTO
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public CountryDTO Country { get; set; }
    }
}
