using HoneyStore.DataAccess.Entities;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        Task<ICollection<Order>> GetOrdersByUserId(int userId);
    }
}