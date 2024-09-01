using InvoiceData.Entities;
using InvoiceData;
using Microsoft.AspNetCore.Mvc;
using InvoiceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceSystem.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly Context _context;

        public InvoicesController(Context context)
        {
            _context = context;
        }

        // GET: Invoices
        public IActionResult Index()
        {
            var invoices = _context.Invoices.Include(i => i.Client)
                .Include(d=>d.InvoiceDetails).ToList();
            return View(invoices);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            var clients = _context.Clients
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            var categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            var model = new CreateInvoiceViewModel
            {
                Clients = clients,
                Categories = categories
            };

            return View(model);
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Clients = _context.Clients
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList();

                model.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList();

                return View(model);
            }

            var invoice = new Invoice
            {
                Client = await _context.Clients.FindAsync(model.ClientId)
            };

            foreach (var detail in model.InvoiceDetails)
            {
                var category = await _context.Categories.FindAsync(detail.CategoryId);
                if (category != null)
                {
                    var invoiceDetail = new InvoiceDetails
                    {
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        Categories = category
                    };
                    invoice.InvoiceDetails.Add(invoiceDetail);
                }
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Categories)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

    }
}
