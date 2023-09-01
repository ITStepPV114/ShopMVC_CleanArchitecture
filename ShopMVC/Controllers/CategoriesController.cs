using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using DataAccess.Data;
using BusinessLogic.Interfaces;

namespace ShopMVC.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ShopMVCDbContext _context;
        private readonly ICategoryService _categoryService;


        public CategoriesController(ShopMVCDbContext context, ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _categoryService.GetAll() != null ? 
                          View(_categoryService.GetAll()) :
                          Problem("Entity set 'ShopMVCDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _categoryService.GetAll() == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _categoryService.GetAll() == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.Edit(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryService.ExistCategory(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _categoryService.GetAll() == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_categoryService.GetAll() == null)
            {
                return Problem("Entity set 'ShopMVCContext.Category'  is null.");
            }
            var category = _categoryService.GetById(id);
            if (category != null)
            {
                _categoryService.Remove(category);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
