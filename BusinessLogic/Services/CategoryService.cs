using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopMVCDbContext _context;
        public CategoryService(ShopMVCDbContext context)
        {
            _context = context;
        }

        public void Create(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void Edit(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }

        public bool ExistCategory(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int? id)
        {
            return _context.Categories.FirstOrDefault(m => m.Id == id);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

        }
    }

}
