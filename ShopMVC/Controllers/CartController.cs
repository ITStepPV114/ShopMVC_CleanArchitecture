using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Helper;

namespace ShopMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopMVCDbContext _context;
        public CartController(ShopMVCDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            if (idList==null) idList = new List<int>(); 
            List<Product> products=idList.Select(id=>_context.Products.Find(id)).ToList();
            return View(products);
        }

        public IActionResult Add(int id) {
            if (_context.Products.Find(id) == null) return NotFound();
            List<int> idList= HttpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) idList = new List<int>();
            idList.Add(id); //add id of product to cart
            HttpContext.Session.SetObject<List<int>>("mycart",idList);
            return RedirectToAction(nameof(Index),"Home");
        }


        public IActionResult Remove(int id)
        {
            if (_context.Products.Find(id) == null) return NotFound();
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) idList = new List<int>();
            idList.Remove(id); //delete id of product to cart
            HttpContext.Session.SetObject<List<int>>("mycart", idList);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
