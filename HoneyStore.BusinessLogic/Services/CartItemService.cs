using AutoMapper;
using HoneyStore.BusinessLogic.Helpers;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class CartItemService: BaseService, ICartItemService
    {
        private readonly IMapper _mapper;

        public CartItemService(IUnitOfWork unitOfWork, IMapperFactory factory) : base(unitOfWork)
        {
            _mapper = factory.CreateMapper();
        }

        public async Task<CartItemDto> GetCartItemAsync(int id)
        {
            var cartItemEntity = await _uow.CartItems.GetAsync(id);

            var cartItemDto = _mapper.Map<CartItem, CartItemDto>(cartItemEntity);

            return cartItemDto;
        }

        public async Task<ICollection<CartItemDto>> GetAllCartItemsAsync()
        {
            var cartItemEntities = await _uow.CartItems.GetAllAsync();

            var cartItemDtos = _mapper.Map<ICollection<CartItem>, ICollection<CartItemDto>>(cartItemEntities);

            return cartItemDtos;
        }

        public async Task<ICollection<CartItemDto>> GetCartItemsByUserId(int userId)
        {
            var cartItemEntities = await _uow.CartItems.GetCartItemsByUserId(userId);

            var cartItemDtos = _mapper.Map<IEnumerable<CartItem>, ICollection<CartItemDto>>(cartItemEntities);

            return cartItemDtos;
        }

        public async Task AddCartItemAsync(CartItemDto cartItem)
        {
            var cartItemEntity = _mapper.Map<CartItemDto, CartItem>(cartItem);

            await _uow.CartItems.AddAsync(cartItemEntity);

            await _uow.SaveAsync();
            cartItem.Id = cartItemEntity.Id;
        }

        public async Task RemoveCartItemAsync(int id)
        {
            var cartItemEntity = await _uow.CartItems.GetAsync(id);

            await _uow.CartItems.RemoveAsync(cartItemEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateCartItemAsync(int id, CartItemDto cartItem)
        {
            var cartItemEntity = _mapper.Map<CartItemDto, CartItem>(cartItem);

            await _uow.CartItems.UpdateAsync(id, cartItemEntity);

            await _uow.SaveAsync();
        }
    }
}