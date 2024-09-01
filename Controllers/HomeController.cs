using InvoiceData;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvoiceSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }




     

        // GET: Dashboard
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                ClientsCount = _context.Clients.Count(),
                CategoriesCount = _context.Categories.Count(),
                InvoicesCount = _context.Invoices.Count()
            };

            return View(model);
        }

    
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
