using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Product
{
    public class ProductAddEditDto
    {
        // wymagane
        // max 100 znakow
        X
        public string Name { get; set; }
        // zakres od 0 do 1000
        X
        public X Quantity { get; set; }
        // zakres od 0 do 1 000 000
        X
        public decimal Price { get; set; }
    }
}
