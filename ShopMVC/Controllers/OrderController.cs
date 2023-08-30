using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Helper;
using System.Security.Claims;
using System.Text.Json;

namespace ShopMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShopMVCDbContext _context;
        public OrderController(ShopMVCDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders= _context.Orders.Where(o=>o.UserId==userId).ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) return BadRequest();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Product> products=idList.Select(id=>_context.Products.Find(id)).ToList();
            Order newOrder= new Order() { 
                OrderDate=DateTime.Now,
                IdsProduct=JsonSerializer.Serialize(idList), 
                TotalPrice=products.Sum(p=>p.Price),
                UserId=userId
                         
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            //HttpContext.Session.Clear();
            HttpContext.Session.Remove("mycart");
            return  RedirectToAction(nameof(Index));    
        }
    }
}
