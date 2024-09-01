using InvoiceData.Entities;
using InvoiceData;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceData.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceSystem.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly Context _context;

        public ClientsController(Context context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var Clients = await _context.Clients.ToListAsync();
            return View(Clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return PartialView("Create", new CreateClientViewModel());
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    Name = model.Name,
                    Notes = model.Notes
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return PartialView("Create", model);
        }

    }
}
