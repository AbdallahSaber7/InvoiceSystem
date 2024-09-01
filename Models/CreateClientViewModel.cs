using System.ComponentModel.DataAnnotations;

namespace InvoiceSystem.Models
{
    public class CreateClientViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Notes { get; set; }
    }
}
