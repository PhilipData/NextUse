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
    public class ProfileRepositoryTests
    {
        private readonly ApplicationDBContext _context;
        private readonly ProfileRepository _repository;

        public ProfileRepositoryTests()
        {
            // Create an in-memory Database
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "ProfileRepositoryTests")
                .Options;

            _context = new ApplicationDBContext(options);
            _repository = new ProfileRepository(_context);

            _context.Database.EnsureDeleted();

            // Seed test data
            _context.Addresses.AddRange(new List<Address>
            {
                new Address
                {
                    Id = 1,
                    Country = "Denmark",
                    City = "Copenhagen",
                    PostalCode = 2400,
                    Street = "Kongens_Nytorv",
                    HouseNumber = "1"
                },
                new Address
                {
                    Id = 2,
                    Country = "Sweden",
                    City = "Malmo",
                    PostalCode = 8000,
                    Street = "Amiralsgatan",
                    HouseNumber = "1"
                }
            });

            _context.Profiles.AddRange(new List<Profile>
            {
                new Profile { Id = 4, Name = "Bob", AddressId = 1 },
                new Profile { Id = 5, Name = "Jane Doe", AddressId = 2 },
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProfiles()
        {
            // Arrange

            // Act
            var profiles = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(profiles);
            Assert.Equal(2, profiles.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProfile_WhenIdExists()
        {
            // Arrange

            // Act
            var profile = await _repository.GetByIdAsync(4);

            // Assert
            Assert.NotNull(profile);
            Assert.Equal(4, profile.Id);

        }

        [Fact]
        public async Task AddAsync_ShouldAddNewProfile()
        {
            // Arrange
            var newProfile = new Profile
            {
                Id = 10,
                Name = "Bob",
                AddressId = 1,
            };

            // Act
            var addedProfile = await _repository.AddAsync(newProfile);
            var profileInDb = await _context.Profiles.FindAsync(addedProfile.Id);

            // Assert
            Assert.NotNull(addedProfile);
            Assert.NotNull(profileInDb);
            Assert.Equal(10, profileInDb.Id);

        }

        [Fact]
        public async Task UpdateByAsync_ShouldUpdateProfile_WhenIdExists()
        {
            // Arrange
            string updatedName = "Tobias";

            var updatedProfile = new Profile
            {
                Name = updatedName
            };

            // Act
            var result = await _repository.UpdateByIdAsync(4, updatedProfile);
            var profileInDb = await _context.Profiles.FindAsync(4);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(profileInDb);
            Assert.Equal(updatedName, profileInDb.Name);

        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteProfile_WhenIdExists()
        {
            // Arrange


            // Act
            await _repository.DeleteByIdAsync(2);
            var profileInDb = await _context.Profiles.FindAsync(2);

            // Assert
            Assert.Null(profileInDb);

        }

    }
}
