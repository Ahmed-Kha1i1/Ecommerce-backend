using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Discount : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDisabled { get; set; } = false;
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
    }
}
