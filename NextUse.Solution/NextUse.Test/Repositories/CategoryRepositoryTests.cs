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
    public class CategoryRepositoryTests
    {
        private readonly ApplicationDBContext _context;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            // Create an in-memory database emulation
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CategoryRepositoryTests")
                .Options;

            _context = new ApplicationDBContext(options);
            _repository = new CategoryRepository(_context);

            //Seed test data
            _context.Categories.AddRange(new List<Category>
            {
                new Category { Id = 1, Name = "clothing"},
                new Category { Id = 2, Name = "CarParts" }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTaskAsync_ShouldReturnAllCategories()
        {
            // Arrange

            // Act
            var categories = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(2, categories.Count());

        }

        [Fact]
        public async Task GetByIdAsync_shouldReturnAllCategories()
        {
            // Arrange

            // Act
            var categories = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(categories);
            Assert.Equal("Clothing", categories.Name);

        }

        [Fact]
        public async Task AddAsync_shouldReturnNewCategory()
        {
            // Arrange
            var newCategory = new Category
            {
                Id = 1,
                Name = "Bob"
            };

            // Act
            var addedCategory = await _repository.AddAsync(newCategory);
            var categoryInDb = await _context.Categories.FindAsync(newCategory.Id);

            // Assert
            Assert.NotNull(newCategory);
            Assert.NotNull(categoryInDb);
            Assert.Equal(10m, categoryInDb.Id);//???? double check

        }

        [Fact]
        public async Task UpdatedByIdAsync_shouldUpdateCategory_WhenIdExists()
        {
            // Arrange
            var updatedCategory = new Category
            {
                Id = 1,
                Name = "Toys"
            };

            // Act
            var result = await _repository.UpdateByIdAsync(4, updatedCategory);
            var categoryInDb = await _context.Categories.FindAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(categoryInDb);
            Assert.Equal("Toys", categoryInDb.Name);

        }

        [Fact]
        public async Task DeleteByAsync_shouldDeleteCategory_WhenIdExists()
        {
            // Arrange

            // Act
            await _repository.DeleteByIdAsync(4);
            var categoryInDb = await _context.Categories.FindAsync(2);

            // Assert
            Assert.NotNull(categoryInDb);
        }
    }
}
