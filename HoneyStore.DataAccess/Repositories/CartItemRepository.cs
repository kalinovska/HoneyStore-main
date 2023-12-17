using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class CartItemRepository: BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task<CartItem> GetAsync(int id)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .ThenInclude(p => p.ProductPhoto)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }


        public async Task<ICollection<CartItem>> GetCartItemsByUserId(int userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .ThenInclude(p => p.ProductPhoto)
                .Where(ci => ci.UserId == userId && ci.OrderId == null)
                .ToListAsync();
        }

        public override async Task UpdateAsync(int id, CartItem cartItem)
        {
            var cartItemFromDb = await _context.CartItems
                .FirstOrDefaultAsync(i => i.Id == id);

            cartItemFromDb.OrderId = cartItem.OrderId;
            cartItemFromDb.Quantity = cartItem.Quantity;

            _context.CartItems.Update(cartItemFromDb);
        }
    }
}