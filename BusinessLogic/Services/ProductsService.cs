using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;

        //private readonly ShopMVCDbContext _context;

        public ProductsService(IRepository<Product> productRepo,
                                IRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
           
        }

        public void Creat(Product product)
        {
            _productRepo.Insert(product);
            _productRepo.Save();
        }

        public void Delete(int? id)
        {

            var product = Get(id);
            if (product != null)
            {
                 _productRepo.Delete(product);
                _productRepo.Save();

            }
        }

        public void Edit(Product product)
        {
            _productRepo.Update(product);
            _productRepo.Save();
        }

        public Product? Get(int? id)
        {
            //return _productRepo.GetByID(id);
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return _productRepo.Get(includeProperties: new[] { "Category"}).ToList();
        }

        public List<Category> GetAllCategory()
        {
           //return _context.Categories.ToList();
           return _categoryRepo.Get().ToList();
        }
    }
}
