using BusinessLogic.Interfaces;
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
        private readonly IOrdersService _ordersService;
        public OrderController(IOrdersService ordersService) 
        {
            _ordersService = ordersService;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _ordersService.GetAll(userId);
            return View(orders);
        }

        public IActionResult Create()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            _ordersService.Create(userId, idList);
            HttpContext.Session.Remove("mycart");
            return  RedirectToAction(nameof(Index));    
        }
    }
}
