using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository.Interface;

namespace NextUse.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _Context;

        public ProductRepository(ApplicationDBContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _Context.Products
                .Include(p => p.Address)
                .Include(p => p.Profile)
                    .ThenInclude(profile => profile == null ? null : profile.Ratings)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(comment => comment.Profile)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _Context.Products
                .Include(p => p.Address)
                .Include(p => p.Profile)
                    .ThenInclude(profile => profile == null ? null : profile.Ratings)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(comment => comment.Profile)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByProfileIdAsync(int profileId)
        {
            return await _Context.Products
                .Include(p => p.Address)
                .Include(p => p.Profile)
                    .ThenInclude(profile => profile == null ? null : profile.Ratings)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(comment => comment.Profile)
                .Include(p => p.Images)
                .Where(p => p.ProfileId == profileId)
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product newProduct)
        {
            await _Context.Products.AddAsync(newProduct);
            await _Context.SaveChangesAsync();
            var product = await GetByIdAsync(newProduct.Id);

            if (product is null)
                throw new Exception("Product wasn't added");

            return product;
        }

        public async Task<Product> UpdateByIdAsync(int id, Product updatedProduct)
        {
            var product = await _Context.Products.FirstOrDefaultAsync(pro => pro.Id == id);
            if (product != null)
            {
                product.Id = updatedProduct.Id;
                product.Title = updatedProduct.Title;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;

                await _Context.SaveChangesAsync();
                return updatedProduct;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await _Context.Products.FirstOrDefaultAsync(pro => pro.Id == id);
            if (product != null)
            {
                _Context.Products.Remove(product);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetByTitleAsync(string title)
        {
            return await _Context.Products
                .Include(p => p.Address)
                .Include(p => p.Profile)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(comment => comment.Profile)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(t => t.Title == title);
        }
    }
}
