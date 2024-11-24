using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Address : BaseEntity
    {
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsDefault { get; set; }
    }
}
