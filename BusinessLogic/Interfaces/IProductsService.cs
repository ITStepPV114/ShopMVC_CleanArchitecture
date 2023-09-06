using BusinessLogic.DTOs;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        List<CategoryDto> GetAllCategory();
        List<ProductDto> GetAll();
       // List<ProductDto> GetByOrderName();
        List<ProductDto> GetByOrderPrice();
        ProductDto? Get(int? id);
        void Creat(ProductDto product);
        void Edit(ProductDto product);
        void Delete(int? id);
    }
}
