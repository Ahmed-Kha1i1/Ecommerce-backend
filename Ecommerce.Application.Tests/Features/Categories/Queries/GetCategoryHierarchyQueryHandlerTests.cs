using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery;
using Moq;

namespace Ecommerce.Application.Tests.Features.Categories.Queries
{
    public class GetCategoryHierarchyQueryHandlerTests
    {
        Mock<ICategoryRepository> categoryRepositoryMock;
        public GetCategoryHierarchyQueryHandlerTests()
        {
            categoryRepositoryMock = new Mock<ICategoryRepository>();
        }
        [Fact]
        public async Task Handle_CategoryFound_ReturnsSuccessResponse()
        {
            // Arrange
            var categoryId = 1;
            var categoryResponse = new GetCategoryHierarchyQueryResponse(); // Assume this is the correct type

            categoryRepositoryMock.Setup(repo => repo.GetCategoryHierarchy(categoryId))
                                  .ReturnsAsync(categoryResponse);

            var handler = new GetCategoryHierarchyQueryHandler(categoryRepositoryMock.Object);
            var request = new GetCategoryHierarchyQuery(categoryId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(categoryResponse, result.Data);
        }

        [Fact]
        public async Task Handle_CategoryNotFound_ReturnsBadRequestResponse()
        {
            // Arrange
            var categoryId = 1;

            categoryRepositoryMock.Setup(repo => repo.GetCategoryHierarchy(categoryId))
                                  .ReturnsAsync((GetCategoryHierarchyQueryResponse?)null);

            var handler = new GetCategoryHierarchyQueryHandler(categoryRepositoryMock.Object);
            var request = new GetCategoryHierarchyQuery(categoryId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal($"Category with ID {categoryId} not found.", result.Message);
        }
    }
}

