using AutoMapper;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;

namespace HoneyStore.BusinessLogic.Profiles
{
    public class ProductProfile: Profile 
    {
        public ProductProfile()//IUnitOfWork uow)
        {
            const string baseUri = "https://localhost:44351";
            CreateMap<Product, ProductDto>()
                //.ForMember(dest => dest.Mark,
               // opt => opt.MapFrom(b => uow.Comments.GetMarkByProductId(b.Id)))
                .ForMember(dest=> dest.ImageUrl, opt=> opt.MapFrom(b=> CreateImageUrl(baseUri, b.ImageUrl)));
            CreateMap<ProductDto, Product>();
        }

        private static string CreateImageUrl(string baseUrl, string imagePath)
        {
            return $"{baseUrl}/{imagePath.Replace("\\", "/")}";
        }
    }
}