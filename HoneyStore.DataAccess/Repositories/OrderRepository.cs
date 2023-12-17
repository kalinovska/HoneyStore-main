using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task<ICollection<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.CartItems)
                .OrderBy(o => o.CreatedOn)
                .ToListAsync();
        }

        public async Task<ICollection<Order>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Include(o => o.CartItems)
                .OrderBy(o => o.CreatedOn)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public override async Task UpdateAsync(int id, Order order)
        {
            var orderFromDb = await _context.Orders.FirstOrDefaultAsync(p => p.Id == id);

            orderFromDb.Status = order.Status;

            _context.Orders.Update(orderFromDb);
        }
    }
}