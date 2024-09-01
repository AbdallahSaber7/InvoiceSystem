using InvoiceData.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceSystem.Models
{
    public class CreateInvoiceViewModel
    {
        public int ClientId { get; set; }
        public List<SelectListItem> Clients { get; set; } = new List<SelectListItem>();

        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; } = new List<InvoiceDetailViewModel>();

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }

    public class InvoiceDetailViewModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
