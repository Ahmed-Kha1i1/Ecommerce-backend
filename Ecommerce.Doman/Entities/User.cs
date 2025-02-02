using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Doman.Entities
{
    public class User : IdentityUser<int>, ISoftDeleteable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enGender? Gender { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
