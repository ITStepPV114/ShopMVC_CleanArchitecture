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
        List<Category> GetAllCategory();
        List<Product> GetAll();
        Product? Get(int? id);
        void Creat(Product product);
        void Edit(Product product);
        void Delete(int? id);
    }
}
