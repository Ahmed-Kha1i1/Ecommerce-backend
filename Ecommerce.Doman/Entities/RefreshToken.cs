using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn is null && !IsExpired;
    }
}
