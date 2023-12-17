using HoneyStore.DataAccess.Entities;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface ICartItemRepository: IGenericRepository<CartItem>
    {
        Task<ICollection<CartItem>> GetCartItemsByUserId(int userId);
    }
}