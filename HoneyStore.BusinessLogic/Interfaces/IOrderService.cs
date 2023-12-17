using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int id);

        Task<ICollection<OrderDto>> GetAllOrdersAsync();

        Task<ICollection<OrderDto>> GetOrdersByUserIdAsync(int userId);

        Task AddOrderAsync(OrderDto order);

        Task RemoveOrderAsync(int id);

        Task UpdateOrderAsync(int id, OrderDto order);
    }
}