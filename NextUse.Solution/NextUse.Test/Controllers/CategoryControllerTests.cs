using Microsoft.AspNetCore.Mvc;
using Moq;
using NextUse.API.Controllers;
using NextUse.Services.DTO.CategoryDTO;
using NextUse.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnOk_WhenCategoryExists()
        {
            // Arrange
            var categories = new List<CategoryResponse> 
            { 
                new CategoryResponse
                {
                    Id = 1,
                    Name = "Test"
                } 
            };
            _mockCategoryService.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetAll();


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCategories = Assert.IsAssignableFrom<IEnumerable<CategoryResponse>>(okResult.Value);
            Assert.Single(returnCategories);

        }
        
        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoCategoryExists()
        {
            // Arrange
            _mockCategoryService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<CategoryResponse>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task GetAll_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var exceptionMessage = "Unable To Reach Database";
            _mockCategoryService.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal(exceptionMessage, badRequestResult.Value);

        }

        [Fact]
        public async Task Add_ReturnsOk_WhenCategoryIsAdded()
        {
            // Arrange
            var newCategory = new CategoryRequest { Name = "Bob" };
            var categoryResponse = new CategoryResponse { Name = "Bob" };

            _mockCategoryService.Setup(service => service.AddAsync(newCategory)).ReturnsAsync(categoryResponse);

            // Act
            var result = await _controller.Add(newCategory);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<CategoryResponse>(okResult.Value);
            Assert.Equal("Bob", returnedCategory.Name);
        }

        [Fact]
        public async Task FindById_ReturnsOk_WhenCategoryExists()
        {
            // Arrange
            var categoryResponse = new CategoryResponse { Id = 1, Name = "Bob" };
            _mockCategoryService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(categoryResponse);

            // Act
            var result = await _controller.FindbyId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCategory = Assert.IsType<CategoryResponse> (okResult.Value);
            Assert.Equal(1, returnedCategory.Id);

        }

        [Fact]
        public async Task FindById_ReturnsNotFound_WhenCategoryDoesntExists()
        {
            // Arrange
            _mockCategoryService.Setup(service => service.GetByIdAsync(2)).ReturnsAsync((CategoryResponse?)null);

            // Act
            var result = await _controller.FindbyId(1);

            // Assert
            var okResult = Assert.IsType<NotFoundResult> (result);
        }

        [Fact]
        public async Task DeleteById_ReturnsOk_WhenCategoryIsDeleted()
        {
            // Arrange


            // Act
            var result = await _controller.DeleteById(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCategoryService.Verify(service => service.DeleteByIdAsync(1), Times.Once());

        }
    }
}
