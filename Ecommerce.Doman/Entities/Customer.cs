namespace Ecommerce.Doman.Entities
{
    public class Customer : Person
    {
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
