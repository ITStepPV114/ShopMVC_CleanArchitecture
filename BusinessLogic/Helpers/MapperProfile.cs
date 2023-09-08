using BusinessLogic.DTOs;
using DataAccess.Entities;
using AutoMapper;


namespace BusinessLogic.Helpers
{
    public class MapperProfile:Profile //AutoMapper
    {
        public MapperProfile() {
            //map Product=> ProductDto
            CreateMap<Product, ProductDto>()
             .ForMember(productDto => productDto.CategoryName, opt => opt.MapFrom(product => product.Category.Name));
            //map ProductDto=>Product
            CreateMap<ProductDto, Product>();
        }
    }
}
