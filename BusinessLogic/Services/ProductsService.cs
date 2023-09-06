using BusinessLogic.DTOs;
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

        public void Creat(ProductDto productDto)
        {
            Product product = new Product()
            {   
                Id=productDto.Id,
                Name=productDto.Name,
                Description=productDto.Description, 
                Price=productDto.Price, 
                ImagePath=productDto.ImagePath,
                CategoryId=productDto.CategoryId,
            };
            _productRepo.Insert(product);
            _productRepo.Save();
        }

        public void Delete(int? id)
        {

            var product = _productRepo.GetByID(id);
            if (product != null)
            {
                _productRepo.Delete(product);
                _productRepo.Save();

            }
        }

        public void Edit(ProductDto productDto)
        {
            Product product = new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImagePath = productDto.ImagePath,
                CategoryId = productDto.CategoryId,
            };
            _productRepo.Update(product);
            _productRepo.Save();
        }

        public ProductDto? Get(int? id)
        {
            // return _productRepo.GetByID(id);
            //return GetAll().FirstOrDefault(x => x.Id == id);
            //return _productRepo.Get(includeProperties: new[] { "Category" }).Where(p=>p.Id==id).SingleOrDefault();
            // return _productRepo.Get(filter:x=>x.Id==id, includeProperties: new[] { "Category" }).SingleOrDefault();
            //return _productRepo.Get(filter: x => x.Id == id, includeProperties: new[] { "Category" }).SingleOrDefault();
            var product= _productRepo.Get(filter: x => x.Id == id, includeProperties: new[] { "Category" }).SingleOrDefault();
            ProductDto productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                CategoryName=product.Category.Name
            };
            return productDto;
        }

       public List<ProductDto> GetAll()
        {   //include properties: LEFT JOIN in SQL
            //return _productRepo.Get(includeProperties: new[] { "Category"}).ToList();

            var products = _productRepo.Get(includeProperties: new[] { "Category" }).ToList();

            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            }).ToList();
        }

        public List<CategoryDto> GetAllCategory()
        {
            //return _context.Categories.ToList();
            //return _categoryRepo.Get().ToList();
            var categories = _categoryRepo.Get();
            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name=category.Name,
                Description=category.Description
            }).ToList();
        }

        public List<ProductDto> GetByOrderPrice() {
            var products= _productRepo.Get(orderBy: q => q.OrderBy(p => p.Price), includeProperties: new[] { "Category" }).ToList();

            return products.Select(product=>new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            }).ToList();
        }
    }
}
