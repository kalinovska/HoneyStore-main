using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(dist => dist.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)));
            CreateMap<Order, OrderDto>()
                .ForMember(dist => dist.ProductIds, opt => opt.MapFrom(src => src.CartItems.Select(ci => ci.ProductId).ToArray()))
                .ForMember(dist => dist.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
