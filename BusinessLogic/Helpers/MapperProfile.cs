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

            CreateMap<Product, EditProductDto>()
           .ForMember(productDto => productDto.CategoryName, opt => opt.MapFrom(product => product.Category.Name));

            CreateMap<Product, EditProductDto>()
          .ForMember(productDto => productDto.Image, opt => opt.Ignore());
            //map ProductDto=>Product
            CreateMap<EditProductDto, Product>();
        }
    }
}
