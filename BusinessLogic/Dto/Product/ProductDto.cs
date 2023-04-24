using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Product
{
    public class ProductDto
    {
        public X Id { get; set; }
        public string Name { get; set; }
        public X Quantity { get; set; }
        public X Price { get; set; }
    }
}
