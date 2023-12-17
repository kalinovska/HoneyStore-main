using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.Api.Profiles
{
    public class WishProfile : Profile
    {
        public WishProfile()
        {
            CreateMap<WishCreationModel, WishDto>()
                .ForMember(dist => dist.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
