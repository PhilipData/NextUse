using Moq;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Service.DTO.CategoryDTO;
using NextUse.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_shouldReturnListOfCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "BobTest1"
                },
                new Category
                {
                    Id = 2,
                    Name = "BobTest2"
                }
            };

            _mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CategoryResponse>>(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetByIdAsync_shouldReturnNull_WhenNotExists()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Category?)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public async Task GetTaskAsync_shouldReturnCategoryResponse_WhenSuccessful()
        {
            // Arrange
            var categoryRequest = new CategoryRequest { Name = "BobTest" };
            var Category = new Category
            {
                Id = 1,
                Name = "BobTest",
                Products = new()
                {
                    new Product
                    {
                        Id = 1,
                        Title = "Old slippers",
                        Description = "Very Smelly",
                        Price = 0.5m,
                        CategoryId = 1,
                        ProfileId = 1,
                        AddressId = 1
                    }
                }

            };

            // Act
            var result = await _service.AddAsync(categoryRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(Category.Name, result.Name);

        }

        [Fact]
        public async Task UpdatedByIdAsync_ShouldReturnUpdatedCategory_WhenSuccessful()
        {
            // Arrange
            var updatedName = "Bob";
            var updatedRequest = new CategoryRequest { Name = updatedName };
            var updatedCategory = new Category
            {
                Id = 1,
                Name = updatedName,
                Products = new()
                {
                    new Product
                    {
                        Id = 1,
                        Title = "Old slippers",
                        Description = "Very Smelly",
                        Price = 0.5m,
                        CategoryId = 1,
                        ProfileId = 1,
                        AddressId = 1
                    }
                }
            };
            _mockCategoryRepository.Setup(repo => repo.UpdateByIdAsync(1, It.IsAny<Category>())).ReturnsAsync(updatedCategory);


            // Act
            var result = await _service.UpdateByIdAsync(1, updatedRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedName, result.Name);

        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallRepositoryMethod()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.DeleteByIdAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteByIdAsync(1);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);

        }
    }
}
