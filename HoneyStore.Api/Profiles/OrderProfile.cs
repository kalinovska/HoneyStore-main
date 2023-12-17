using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreationModel, OrderDto>()
                .ForMember(dist => dist.CreatedOn, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dist => dist.Status, opt => opt.MapFrom(src => OrderStatus.Created))
                .ReverseMap();
        }
    }
}
