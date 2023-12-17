using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(c => c.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(c => c.User.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(c => c.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(c=>c.User.Email));
            CreateMap<CommentDto, Comment>();
        }
    }
}