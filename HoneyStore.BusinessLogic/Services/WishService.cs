using AutoMapper;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class WishService: BaseService, IWishService
    {
        private readonly IMapper _mapper;

        public WishService(IUnitOfWork uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }

        public async Task<WishDto> GetWishAsync(int userId, int productId)
        {
            var wishEntity = await _uow.Wishes.GetAsync(userId, productId);

            var wishDto = _mapper.Map<Wish, WishDto>(wishEntity);

            return wishDto;
        }

        public async Task<ICollection<WishDto>> GetAllWishesAsync()
        {
            var wishEntities = await _uow.Wishes.GetAllAsync();

            var wishDtos = _mapper.Map<IEnumerable<Wish>, ICollection<WishDto>>(wishEntities);

            return wishDtos;
        }

        public async Task<ICollection<WishDto>> GetWishesByUserIdAsync(int userId)
        {
            var wishEntities = await _uow.Wishes.GetWishesByUserIdAsync(userId);

            var wishDtos = _mapper.Map<IEnumerable<Wish>, ICollection<WishDto>>(wishEntities);

            return wishDtos;
        }

        public async Task AddWishAsync(WishDto wish)
        {
            var wishEntity = _mapper.Map<WishDto, Wish>(wish);

            await _uow.Wishes.AddAsync(wishEntity);

            await _uow.SaveAsync();
        }

        public async Task RemoveWishAsync(int userId, int productId)
        {
            var wishEntity = await _uow.Wishes.GetAsync(userId, productId);

            await _uow.Wishes.RemoveAsync(wishEntity);

            await _uow.SaveAsync();
        }
    }
}