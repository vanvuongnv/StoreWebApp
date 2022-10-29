using System.ComponentModel.DataAnnotations;

namespace Store.Api.Models
{
    public class ProductDtoModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string? ProductName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }
    }
}
