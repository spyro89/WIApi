
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Order
{
    public class OrderAddDto
    {
        // wymagane
        // max 50 znakow
        // adres e-mail
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        // wymagane
        [Required]
        public List<OrderItemAddDto> Items { get; set; }
    }
}
