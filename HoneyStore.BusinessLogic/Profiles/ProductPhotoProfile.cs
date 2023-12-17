using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class ProductPhotoProfile : Profile
    {
        public ProductPhotoProfile()
        {
            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
        }
    }
}
