using Microsoft.EntityFrameworkCore;
using NextUse.DAL.Database.Entities;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDBContext _context;

        public ImageRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Images.ToListAsync(); 
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<IEnumerable<Image>?> GetByProductIdAsync(int productId)
        {
            return await _context.Images.Where(i => i.ProductId == productId).ToListAsync(); 
        }

        public async Task<IEnumerable<Image>> AddRangeAsync(IEnumerable<Image> newImages)
        {
            var productId = newImages.First().ProductId;

            await _context.Images.AddRangeAsync(newImages);
            await _context.SaveChangesAsync();

            var images = await GetByProductIdAsync(productId);

            if (images is null)
                throw new Exception("Image wasn't added");

            return images;
        }

        public async Task<Image> UpdateByIdAsync(int id, Image updatedImage)
        {
            var image = await GetByIdAsync(id);

            if (image is null)
                throw new Exception("Image not found");

            image.Blob = updatedImage.Blob;

            await _context.SaveChangesAsync();
            return image;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
