using Moq;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Services.DTO.RatingDTO;
using NextUse.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Services
{
    public class RatingServiceTests
    {
        private readonly Mock<IRatingRepository> _mockRatingRepository;
        private readonly RatingService _ratingService;

        public RatingServiceTests()
        {
            _mockRatingRepository = new Mock<IRatingRepository>();
            _ratingService = new RatingService(_mockRatingRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfRatings()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating
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
                },
                new Rating
                {
                    Id = 2,
                    Score = 2,
                    FromProfile = new()
                    {
                        Id = 2,
                        Name = "bob"
                    },
                    ToProfile = new()
                    {
                        Id = 1,
                        Name = "John Doe"
                    }
                },
            };

            _mockRatingRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(ratings);

            // Act
            var result = await _ratingService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<RatingResponse>>(result);
            Assert.Equal(2, result.Count());
        }



        [Fact]
        public async Task GetByIdAsync_ShouldReturnRating_WhenExists()
        {
            // Arrange
            var rating = new Rating
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
            _mockRatingRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(rating);

            // Act
            var result = await _ratingService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            _mockRatingRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Rating?)null);

            // Act
            var result = await _ratingService.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnRatingResponse_WhenSuccessful()
        {
            // Arrange
            var ratingRequest = new RatingRequest { Score = 1, FromProfileId = 1, ToProfileId = 2 };
            var rating = new Rating
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

            _mockRatingRepository.Setup(repo => repo.AddAsync(It.IsAny<Rating>())).ReturnsAsync(rating);

            // Act
            var result = await _ratingService.AddAsync(ratingRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RatingResponse>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(rating.Score, result.Score);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowInvalidOperationException_WhenFromProfileIdAndToProfileIdAlreadyExists()
        {
            // Arrange
            var ratingRequest = new RatingRequest { Score = 3, FromProfileId = 1, ToProfileId = 2 };

            _mockRatingRepository.Setup(repo => repo.AlreadyRated(ratingRequest.FromProfileId, ratingRequest.ToProfileId))
                .ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _ratingService.AddAsync(ratingRequest));

            Assert.Equal("A rating already exists for the given profiles.", exception.Message);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowInvalidOperationException_WhenFromProfileIdAndToProfileIdAreEqual()
        {
            // Arrange
            var ratingRequest = new RatingRequest { Score = 3, FromProfileId = 1, ToProfileId = 1 };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _ratingService.AddAsync(ratingRequest));

            Assert.Equal("FromProfileId and ToProfileId must not be equal", exception.Message);
        }

        [Fact]
        public async Task UpdateByIdAsync_ShouldReturnUpdatedRating_WhenSuccessful()
        {
            int updatedScore = 1;

            // Arrange
            var updatedRequest = new RatingRequest { Score = updatedScore };
            var rating = new Rating
            {
                Id = 1,
                Score = 1,
                FromProfileId = 1,
                FromProfile = new()
                {
                    Id = 1,
                    Name = "Bob"
                },
                ToProfileId = 2,
                ToProfile = new()
                {
                    Id = 2,
                    Name = "Jane Doe"
                }
            };

            _mockRatingRepository.Setup(repo => repo.UpdateByIdAsync(1, It.IsAny<Rating>())).ReturnsAsync(rating);

            // Act
            var result = await _ratingService.UpdateByIdAsync(1, updatedRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedScore, result.Score);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallRepositoryMethod()
        {
            // Arrange
            _mockRatingRepository.Setup(repo => repo.DeleteByIdAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _ratingService.DeleteByIdAsync(1);

            // Assert
            _mockRatingRepository.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);
        }
    }
}
