namespace Ecommerce.Doman.Entities
{
    public class Customer : User
    {
        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
