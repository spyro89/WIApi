using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
