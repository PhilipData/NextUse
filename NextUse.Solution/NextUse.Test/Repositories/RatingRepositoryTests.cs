using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Repositories
{
    public class RatingRepositoryTests
    {
        private readonly ApplicationDBContext _context;
        private readonly RatingRepository _ratingRepository;

        public RatingRepositoryTests()
        {
            // Create an in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "RatingRepositoryTests")
                .Options;

            _context = new ApplicationDBContext(options);
            _ratingRepository = new RatingRepository(_context);

            _context.Database.EnsureDeleted();

            // Seed test data
            _context.Profiles.AddRange(new List<Profile>
            {
                new Profile
                {
                    Id = 1,
                    Name = "Bob",
                },
                new Profile
                {
                    Id = 2,
                    Name = "Jane Doe",
                },
                new Profile
                {
                    Id = 1,
                    Name = "Chuck Testa",
                },
            });

            _context.Ratings.AddRange(new List<Rating>
            {
                new Rating { Id = 1, Score = 1, FromProfileId = 1, ToProfileId = 2 },
                new Rating { Id = 2, Score = 2, FromProfileId = 2, ToProfileId = 1 },

            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRatings()
        {
            // Arrange

            // Act
            var ratings = await _ratingRepository.GetAllAsync();

            // Assert
            Assert.NotNull(ratings);
            Assert.Equal(2, ratings.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRating_WhenIdExists()
        {
            // Arrange

            // Act
            var rating = await _ratingRepository.GetByIdAsync(2);

            // Assert
            Assert.NotNull(rating);
            Assert.Equal(2, rating.Id);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewRating()
        {
            // Arrange
            var newRating = new Rating { Id = 3, Score = 3, FromProfileId = 3, ToProfileId = 1 };

            // Act
            var addedRating = await _ratingRepository.AddAsync(newRating);
            var ratingInDb = await _context.Ratings.FindAsync(addedRating.Id);

            // Assert
            Assert.NotNull(addedRating);
            Assert.NotNull(ratingInDb);
            Assert.Equal(3, ratingInDb.Id);
        }

        [Fact]
        public async Task UpdateByIdAsync_ShouldUpdateRating_WhenIdExists()
        {
            int updatedScore = 5;

            // Arrange
            var updatedRating = new Rating
            {
                Score = 5
            };

            // Act
            var result = await _ratingRepository.UpdateByIdAsync(1, updatedRating);
            var ratingInDb = await _context.Ratings.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(ratingInDb);
            Assert.Equal(updatedScore, ratingInDb.Score);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteRating_WhenIdExists()
        {
            // Arrange

            // Act
            await _ratingRepository.DeleteByIdAsync(2);
            var ratingInDb = await _context.Ratings.FindAsync(2);

            // Assert
            Assert.Null(ratingInDb);
        }
    }
}
