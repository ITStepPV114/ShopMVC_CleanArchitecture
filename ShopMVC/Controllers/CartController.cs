using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Helper;

namespace ShopMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Index()
        {
           return View(_cartService.GetProducts());
        }

        public IActionResult Add(int id) {
            _cartService.Add(id);   
            return RedirectToAction(nameof(Index),"Home");
        }


        public IActionResult Remove(int id)
        {
            _cartService.Remove(id);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
