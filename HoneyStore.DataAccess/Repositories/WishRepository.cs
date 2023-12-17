using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class WishRepository: BaseRepository<Wish>, IWishRepository
    {
        public WishRepository(StoreDbContext context) : base(context)
        {

        }

        public async Task<Wish> GetAsync(int userId, int productId)
        {
            return await _context.Wishes
                .Include(w => w.Product)
                .Include(w => w.User)
                .Where(w => w.UserId == userId && w.ProductId == productId)
                .FirstAsync();
        }
        public async Task<ICollection<Wish>> GetWishesByUserIdAsync(int userId)
        {
            return await _context.Wishes
                .Include(w => w.Product)
                .ThenInclude(p => p.ProductPhoto)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }
        
        public async Task UpdateAsync(Wish wish)
        {
            throw new NotImplementedException();
        }
    }
}