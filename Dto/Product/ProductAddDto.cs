using System.ComponentModel.DataAnnotations;

namespace Dto.Product
{
    public class ProductAddEditDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, short.MaxValue)]
        public short Quantity { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
    }
}
