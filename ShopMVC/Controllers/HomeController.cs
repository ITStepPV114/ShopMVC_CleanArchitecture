using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Helper;
using ShopMVC.Models;
using System.Diagnostics;

namespace ShopMVC.Controllers
{
    //[Controller]
    //public class Home
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly List<Product> _products;
        private readonly ShopMVCDbContext _context;

        public HomeController(ILogger<HomeController> logger, ShopMVCDbContext context)
        {
            _logger = logger;
            _context = context;
           
            //_products = SeedData.Products;
        }

        //ViewData
        //ViewBag
        //Mode view
        // 
        public IActionResult Index(int? category_id)
        {
            //Example using Cookies
            //append Cookie
            //HttpContext.Response.Cookies.Append("name", "Tetiana");
            //get Cookie
            //ViewBag.NameAuthor = HttpContext.Request.Cookies["name"];
            //delete Cookie
            //HttpContext.Response.Cookies.Delete("name");

            List<Category> categories = _context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "All", Description = "All Products" });
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;
            var products = _context.Products.Include(product => product.Category).ToList();
            if (category_id!=null && category_id > 0)
            {
                products = products.Where(p => p.CategoryId == category_id).ToList();

            }

            var productsCartViewModel = products.Select(
                p=>new ProductCartViewModel { 
                    Product=p,
                    IsInCart=IsProductInCart(p.Id)
                    }
                ).ToList();

            //for defination active link or disabled
            if (category_id == null)
            {
                ViewBag.ActiveCategoryId = 0;
            }
            else {
                ViewBag.ActiveCategoryId = category_id;
            }
           
            return View(productsCartViewModel);

        }

        private bool IsProductInCart(int id) {
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            if (idList == null) return false;
            return idList.Contains(id);
        }

        //public string Index(string name="Guest")
        //{
        //   return $"Hello, {name}";
        //}


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            var services = HttpContext.RequestServices.GetServices<UserManager<User>>();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}