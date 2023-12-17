using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface IWishService
    {
        Task<WishDto> GetWishAsync(int userId, int productId);

        Task<ICollection<WishDto>> GetAllWishesAsync();

        Task<ICollection<WishDto>> GetWishesByUserIdAsync(int userId);

        Task AddWishAsync(WishDto wish);

        Task RemoveWishAsync(int userId, int productId);
    }
}