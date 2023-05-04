using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Product
{
    public class ProductAddEditDto
    {
        // wymagane
        // max 100 znakow
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        // zakres od 0 do 1000
        [Range(0, 1000)]
        public short Quantity { get; set; }
        // zakres od 0 do 1 000 000
        [Range(0, 1000000)]
        public decimal Price { get; set; }
    }
}
