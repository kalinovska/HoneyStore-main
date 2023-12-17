using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreationModel, ProductDto>();
        }
    }
}
