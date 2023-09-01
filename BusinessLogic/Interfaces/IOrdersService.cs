
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetAll(string userId);
        //Order GetById(int id, string userId);
        void Create(string userId, List<int> idList);
    }
}
