using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task<ICollection<Category>> GetAllAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task UpdateAsync(int id, Category category)
        {
            var categoryFromDb = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            categoryFromDb.Name = category.Name;

            _context.Categories.Update(categoryFromDb);
        }
    }
}