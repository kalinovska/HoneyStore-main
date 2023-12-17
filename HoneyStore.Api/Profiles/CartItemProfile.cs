using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.Api.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreationModel, CartItemDto>()
                .ForMember(dist => dist.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
