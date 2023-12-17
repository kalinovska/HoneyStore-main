using HoneyStore.DataAccess.Entities;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface IWishRepository
    {
        Task<Wish> GetAsync(int userId, int productId);

        Task<ICollection<Wish>> GetAllAsync();

        Task<ICollection<Wish>> GetWishesByUserIdAsync(int userId);

        Task AddAsync(Wish wish);

        Task UpdateAsync(Wish wish);

        Task RemoveAsync(Wish wish);
    }
}