using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace ShopMVC.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ShopMVCDbContext _context;
        public OrdersService(ShopMVCDbContext context)
        {
            _context = context;
        }
        public void Create(string userId, List<int> idList)
        {            
            List<Product> products = idList.Select(id => _context.Products.Find(id)).ToList();
            Order newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                IdsProduct = JsonSerializer.Serialize(idList),
                TotalPrice = products.Sum(p => p.Price),
                UserId = userId
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        //public Order GetById(int id, string userId)
        //{
            
        //}
    }
}
