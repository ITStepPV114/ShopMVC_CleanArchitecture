using BusinessLogic.DTOs;
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
        IEnumerable<ProductDto> GetProducts();
        void Add(int id);
        void Remove(int id);
    }
}
