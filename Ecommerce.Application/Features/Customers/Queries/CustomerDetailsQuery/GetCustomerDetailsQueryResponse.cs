namespace Ecommerce.Application.Features.Customers.Queries.CustomerDetailsQuery
{
    public class GetCustomerDetailsQueryResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool HasDefaultAddress { get; set; }
    }
}
