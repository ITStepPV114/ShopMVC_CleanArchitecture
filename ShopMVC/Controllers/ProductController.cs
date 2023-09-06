using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
//using DataAccess.Entities;
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
        private readonly IProductsService _productsServices;

        public ProductController(IProductsService productsServices)
        {
                _productsServices = productsServices;
        }

        public IActionResult Index()
        {
            List<CategoryDto> categories=_productsServices.GetAllCategory().ToList();
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;
            //TODO: dbcontext
            var products = _productsServices.GetAll(); //products DTOs

            return View(products);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id, string returnUrl = null) { 
            //find in DataBase
            var productDto =_productsServices.Get(id);
            if (productDto == null) return NotFound();
            ViewBag.ReturnUrl = returnUrl;
            return View(productDto);
        }

        public IActionResult Delete(int? id)
        {
            //find in DataBase
            _productsServices.Delete(id);
            return RedirectToAction(nameof(Index), "Home");
           // return Redirect("~/Home/Index");
                    
        }

        [HttpGet]
        public IActionResult Create() {
            var categories =_productsServices.GetAllCategory();
            ViewBag.ListCategory = new SelectList(categories,"Id","Name");
           return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = _productsServices.GetAllCategory();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name");
                return View(productDto);
            }
            _productsServices.Creat(productDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id) {

            var product = _productsServices.Get(id);
            if (product != null)
            {
                var categories = _productsServices.GetAllCategory();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name",product.CategoryId);
                return View(product);

            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Edit(ProductDto productDto)
        {
            _productsServices.Edit(productDto);
            return RedirectToAction("Index");
        }
    }
}
