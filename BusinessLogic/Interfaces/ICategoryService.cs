using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int? id);
        void Create(Category category);
        void Edit(Category category);
        void Remove(Category category);
        bool ExistCategory(int id);
    }
}
