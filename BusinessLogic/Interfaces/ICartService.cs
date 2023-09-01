using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICartService
    {
        IEnumerable<Product> GetProducts();
        void Add(int id);
        void Remove(int id);
    }
}
