using Moq;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Repository.Interface;
using NextUse.Services.DTO.ProfileDTO;
using NextUse.Services.Services;


namespace NextUse.Test.Services
{
    public class ProfileServiceTests
    {
        private readonly Mock<IProfileRepository> _mockProfileRepository;
        private readonly ProfileService _profileService;

        public ProfileServiceTests()
        {
            _mockProfileRepository = new Mock<IProfileRepository>();
            _profileService = new ProfileService(_mockProfileRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListofProfiles()
        {
            // Arrange
            var profiles = new List<Profile>
            {
                new Profile
                {
                    Id = 1,
                    Name = "Bob",
                    Address = new()
                    {
                        Id = 1,
                        Country = "Denmark",
                        City = "Copenhagen",
                        PostalCode = 2400,
                        Street = "Kongens_Nytorv",
                        HouseNumber = "1"
                    }
                },
                new Profile
                {
                    Id = 2,
                    Name = "Jane Doe",
                    Address = new()
                    {
                        Id = 2,
                        Country = "Sweden",
                        City = "Malmo",
                        PostalCode = 8000,
                        Street = "Amiralsgatan",
                        HouseNumber = "1"
                    }
                }
            };

            _mockProfileRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(profiles);

            // Act
            var result = await _profileService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProfileResponse>>(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProfile_WhenExists()
        {
            // Arrange
            var profile = new Profile
            {
                Id = 1,
                Name = "Bob",
                Address = new()
                {
                    Id = 1,
                    Country = "Denmark",
                    City = "Copenhagen",
                    PostalCode = 2400,
                    Street = "Kongens_Nytorv",
                    HouseNumber = "1"
                }
            };

            _mockProfileRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(profile);

            // Act
            var result = await _profileService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProfileDoesntExists()
        {
            // Arrange
            _mockProfileRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Profile?)null);

            // Act
            var result = await _profileService.GetByIdAsync(99);

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public async Task AddAsync_ShouldReturnProfileResponse_WhenSuccessful()
        {
            // Arrange
            var profileRequest = new ProfileRequest { Name = "Bob" };
            var profile = new Profile
            {
                Id = 1,
                Name = "Bob",
                Address = new()
                {
                    Id = 1,
                    Country = "Denmark",
                    City = "Copenhagen",
                    PostalCode = 2400,
                    Street = "Kongens_Nytorv",
                    HouseNumber = "1"
                }
            };

            _mockProfileRepository.Setup(repo => repo.AddAsync(It.IsAny<Profile>())).ReturnsAsync(profile);

            // Act
            var result = await _profileService.AddAsync(profileRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(profile.Name, result.Name);

        }

        [Fact]
        public async Task UpdatedByIdAsync_ShouldReturnUpdatedProfile_WhenSuccessful()
        {
            // Arrange
            var updatedName = "Elvis";
            var updatedRequest = new ProfileRequest { Name = updatedName };
            var updatedProfile = new Profile
            {
                Id = 1,
                Name = updatedName,
                Address = new()
                {
                    Id = 1,
                    Country = "Denmark",
                    City = "Copenhagen",
                    PostalCode = 2400,
                    Street = "Kongens_Nytorv",
                    HouseNumber = "1"
                }
            };

            _mockProfileRepository.Setup(repo => repo.UpdateByIdAsync(1, It.IsAny<Profile>())).ReturnsAsync(updatedProfile);

            // Act
            var result = await _profileService.UpdateByIdAsync(1, updatedRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedName, result.Name);

        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallRepositoryFunction() // Eller Method
        {
            // Arrange
            _mockProfileRepository.Setup(repo => repo.DeleteByIdAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _profileService.DeleteByIdAsync(1);

            // Assert
            _mockProfileRepository.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);

        }
    }
}
