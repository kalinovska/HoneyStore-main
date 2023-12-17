using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task<Product> GetAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.ProductPhoto)
                .Include(c => c.Category)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public override async Task<ICollection<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.ProductPhoto)
                .Include(c => c.Category)
                .Include(p => p.Comments)
                .ToListAsync();
        }

        public async Task<ICollection<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.ProductPhoto)
                .Include(c => c.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<ICollection<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products
                .Include(p => p.ProductPhoto)
                .Include(p => p.Producer)
                .Include(c => c.Category)
                .Include(p => p.Comments)
                .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public override async Task UpdateAsync(int id, Product product)
        {
            var productFromDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            productFromDb.Name = product.Name;
            productFromDb.Price = product.Price;
            productFromDb.Description = product.Description;
            productFromDb.ImageUrl = product.ImageUrl;
            productFromDb.CommentsEnabled = product.CommentsEnabled;

            _context.Products.Update(productFromDb);
        }
    }
}