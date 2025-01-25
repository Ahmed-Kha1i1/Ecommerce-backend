using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Tests
{
    public class DependencyTests
    {
        private static readonly List<string> DisallowedAssemblies = new List<string>
        {
            "Ecommerce.API",
            "Ecommerce.Application",
            "Ecommerce.Infrastructure",
            "Ecommerce.Persistent",
        };

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnAtherProducts()
        {
            // Arrange
            var domainAssembly = typeof(BaseEntity).Assembly;

            var referencedAssemblies = domainAssembly.GetReferencedAssemblies();

            // Act
            var invalidReferences = referencedAssemblies
                .Where(ra =>
                    DisallowedAssemblies.Any(da => ra.Name.Equals(da, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();


            // Assert
            Assert.Empty(invalidReferences);
        }
    }
}
