using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItemDto> GetCartItemAsync(int id);

        Task<ICollection<CartItemDto>> GetAllCartItemsAsync();

        Task<ICollection<CartItemDto>> GetCartItemsByUserId(int userId);

        Task AddCartItemAsync(CartItemDto cartItem);

        Task RemoveCartItemAsync(int id);

        Task UpdateCartItemAsync(int id, CartItemDto cartItem);
    }
}