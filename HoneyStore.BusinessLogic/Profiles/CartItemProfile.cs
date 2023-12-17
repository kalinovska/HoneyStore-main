using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class CartItemProfile: Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDto>();

            CreateMap<CartItemDto, CartItem>()
                .ForMember(dist => dist.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
