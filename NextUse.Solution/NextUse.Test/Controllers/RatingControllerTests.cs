using Microsoft.AspNetCore.Mvc;
using Moq;
using NextUse.API.Controllers;
using NextUse.Service.DTO.RatingDTO;
using NextUse.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Controllers
{
    public class RatingControllerTests
    {
        private readonly Mock<IRatingService> _mockRatingService;
        private readonly RatingController _controller;

        public RatingControllerTests()
        {
            _mockRatingService = new Mock<IRatingService>();
            _controller = new RatingController(_mockRatingService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenRatingsExist()
        {
            // Arrange
            var ratings = new List<RatingResponse>
        {
            new RatingResponse
            {
                Id = 1,
                Score = 1,
                FromProfile = new()
                {
                    Id = 1,
                    Name = "Bob"
                },
                ToProfile = new()
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            }
        };
            _mockRatingService.Setup(service => service.GetAllAsync()).ReturnsAsync(ratings);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRatings = Assert.IsAssignableFrom<IEnumerable<RatingResponse>>(okResult.Value);
            Assert.Single(returnedRatings);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoRatingsExist()
        {
            // Arrange
            _mockRatingService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<RatingResponse>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task Add_ShouldReturnOk_WhenRatingIsAdded()
        {
            // Arrange
            var newRating = new RatingRequest { Score = 1, FromProfileId = 1, ToProfileId = 2 };
            var ratingResponse = new RatingResponse
            {
                Id = 1,
                Score = 1,
                FromProfile = new()
                {
                    Id = 1,
                    Name = "Bob"
                },
                ToProfile = new()
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            };

            _mockRatingService.Setup(service => service.AddAsync(newRating)).ReturnsAsync(ratingResponse);

            // Act
            var result = await _controller.Add(newRating);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRating = Assert.IsType<RatingResponse>(okResult.Value);
            Assert.Equal(1, returnedRating.Score);
        }

        [Fact]
        public async Task FindById_ShouldReturnOk_WhenRatingExists()
        {
            // Arrange
            var ratingResponse = new RatingResponse
            {
                Id = 1,
                Score = 1,
                FromProfile = new()
                {
                    Id = 1,
                    Name = "Bob"
                },
                ToProfile = new()
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            };
            _mockRatingService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(ratingResponse);

            // Act
            var result = await _controller.FindbyId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRating = Assert.IsType<RatingResponse>(okResult.Value);
            Assert.Equal(1, returnedRating.Id);
        }

        [Fact]
        public async Task FindById_ShouldReturnNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            _mockRatingService.Setup(service => service.GetByIdAsync(99)).ReturnsAsync((RatingResponse?)null);

            // Act
            var result = await _controller.FindbyId(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task UpdateById_ShouldReturnOk_WhenRatingIsUpdated()
        {
            // Arrange
            var updatedRating = new RatingRequest { Score = 1, FromProfileId = 1, ToProfileId = 2 };
            var ratingResponse = new RatingResponse
            {
                Id = 1,
                Score = 1,
                FromProfile = new()
                {
                    Id = 1,
                    Name = "Bob"
                },
                ToProfile = new()
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            };

            _mockRatingService.Setup(service => service.UpdateByIdAsync(1, updatedRating)).ReturnsAsync(ratingResponse);

            // Act
            var result = await _controller.UpdatedById(1, updatedRating);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRating = Assert.IsType<RatingResponse>(okResult.Value);
            Assert.Equal(ratingResponse.Score, returnedRating.Score);
        }

        [Fact]
        public async Task UpdateById_ShouldReturnBadRequest_WhenRatingDoesNotExist()
        {
            // Arrange
            var updatedRating = new RatingRequest { Score = 1, FromProfileId = 1, ToProfileId = 2 };

            _mockRatingService.Setup(service => service.UpdateByIdAsync(99, updatedRating)).ThrowsAsync(new Exception("Rating not found"));

            // Act
            var result = await _controller.UpdatedById(99, updatedRating);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteById_ShouldReturnOk_WhenRatingIsDeleted()
        {
            // Act
            var result = await _controller.DeleteById(1);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockRatingService.Verify(service => service.DeleteByIdAsync(1), Times.Once);
        }

    }
}
