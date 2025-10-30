using Microsoft.AspNetCore.Mvc;
using Moq;
using NextUse.API.Controllers;
using NextUse.Service.DTO.ProfileDTO;
using NextUse.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.Test.Controllers
{
    public class ProfileControllerTests
    {
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly ProfileController _Controller;

        public ProfileControllerTests()
        {
            _mockProfileService = new Mock<IProfileService>();
            _Controller = new ProfileController(_mockProfileService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenProfilesExists()
        {
            // Arrange
            var profiles = new List<ProfileResponse>
            {
                new ProfileResponse
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
                        Housenumber = "1"
                    }
                }
            };
            _mockProfileService.Setup(service => service.GetAllAsync()).ReturnsAsync(profiles);
            // Act
            var result = await _Controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProfiles = Assert.IsAssignableFrom<IEnumerable<ProfileResponse>>(okResult.Value);
            Assert.Single(returnedProfiles);

        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoProfilesExists()
        {
            // Arrange
            _mockProfileService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<ProfileResponse>());

            // Act
            var result = await _Controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenProfileIsAdded()
        {
            // Arrange
            var newProfile = new ProfileRequest { Name = "Bob", AddressId = 1 };
            var profileResponse = new ProfileResponse
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
                    Housenumber = "1"
                }
            };
            _mockProfileService.Setup(service => service.AddAsync(newProfile)).ReturnsAsync(profileResponse);)

            // Act
            var result = await _Controller.Add(newProfile);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProfile = Assert.IsType<ProfileResponse>(okResult.Value);
            Assert.Equal("Bob", returnedProfile.Name);

        }

        [Fact]
        public async Task FindById_ShouldReturnOk_WhenProfileExists()
        {
            // Arrange
            var profileResponse = new ProfileResponse
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
                    Housenumber = "1"
                }
            };
            _mockProfileService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(profileResponse);

            // Act
            var result = await _Controller.FindById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProfile = Assert.IsType<ProfileResponse>(okResult.Value);
            Assert.Equal(1, returnedProfile.Id);
        }

        [Fact]
        public async Task FindById_ShouldReturnNotFound_WhenProfileDoesNotExists()
        {
            // Arrange
            _mockProfileService.Setup(service => service.GetByIdAsync(99)).ReturnsAsync((ProfileResponse?)null);


            // Act
            var result = await _Controller.FindById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task UpdateById_ShouldReturnOk_WhenProfileIsUpdated()
        {
            // Arrange
            var updatedProfile = new ProfileRequest { Name = "Bob", AddressId = 1 };
            var profileResponse = new ProfileResponse
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
                    Housenumber = "1"
                }
            };
            _mockProfileService.Setup(service => service.UpdateByIdAsync(1, updatedProfile)).ReturnsAsync(profileResponse);


            // Act
            var result = await _Controller.UpdateById(1, updatedProfile);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProfile = Assert.IsType<ProfileResponse>(okResult.Value);
            Assert.Equal(profileResponse.Name, returnedProfile.Name);

        }

        [Fact]
        public async Task UpdatedById_ShouldReturnBadRequest_WhenProfileDoesNotExists()
        {
            // Arrange
            var updatedProfile = new ProfileRequest { Name = "Bob", AddressId = 1};
            var profileResponse = new ProfileResponse
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
                    Housenumber = "1"
                }
            };
            _mockProfileService.Setup(service => service.UpdateByIdAsync(99, updatedProfile)).ThrowsAsync(new Exception("Profile not found"));

            // Act
            var result = await _Controller.UpdateById(99, updatedProfile);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task DeleteById_ShouldReturnOk_WhenProfileIsDeleted()
        {
            // Arrange

            // Act
            var reult = await _Controller.DeleteById(1);

            // Assert
            Assert.IsType<OkResult>(reult);
            _mockProfileService.Verify(service => service.DeleteByIdAsync(1), Times.Once);

        }
    }
}
