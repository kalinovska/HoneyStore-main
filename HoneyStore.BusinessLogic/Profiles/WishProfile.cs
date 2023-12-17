using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class WishProfile: Profile
    {
        public WishProfile()
        {
            CreateMap<Wish, WishDto>().ReverseMap();
        }
    }
}