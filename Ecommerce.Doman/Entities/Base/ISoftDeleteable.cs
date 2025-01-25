namespace Ecommerce.Doman.Entities.Base
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
