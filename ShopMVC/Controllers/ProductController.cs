using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Models;

namespace ShopMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ShopMVCDbContext _context;
        private readonly IProductsService productsServices;

        public ProductController(ShopMVCDbContext context,IProductsService productsServices)
        {
            _context =context ;
            this.productsServices = productsServices;
        }

        public IActionResult Index()
        {
            List<Category> categories=_context.Categories.ToList();
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;
            //TODO: dbcontext
            var products = productsServices.GetAll();

            return View(products);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id, string returnUrl = null) { 
            //find in DataBase
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id==id);
            if (product == null) return NotFound();
            ViewBag.ReturnUrl = returnUrl;
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            //find in DataBase
            var product =productsServices.Get(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
               
            }
            return RedirectToAction(nameof(Index), "Home");
           // return Redirect("~/Home/Index");
                    
        }

        [HttpGet]
        public IActionResult Create() {
            var categories =productsServices.GetAllCategory();
            ViewBag.ListCategory = new SelectList(categories,"Id","Name");
           return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = productsServices.GetAllCategory();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name");
                return View(product);
            }
            productsServices.Creat(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id) {

            var product = productsServices.Get(id);
            if (product != null)
            {
                var categories = productsServices.GetAllCategory();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name",product.CategoryId);
                return View(product);

            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productsServices.Edit(product);
            return RedirectToAction("Index");
        }
    }
}
