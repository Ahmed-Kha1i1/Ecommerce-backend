using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public enGender Gender { get; set; } = enGender.Male;
        public DateOnly BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
