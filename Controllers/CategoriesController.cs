using InvoiceData;
using InvoiceData.Entities;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Controllers
{
    [Authorize]

    public class CategoriesController : Controller
    {
        private readonly Context _context;

        public CategoriesController(Context context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return PartialView("Create", new CreateCategoryViewModel());
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Notes = model.Notes
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return PartialView("Create", model);
        }


    }
}
