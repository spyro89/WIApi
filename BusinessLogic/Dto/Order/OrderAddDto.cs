
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Order
{
    public class OrderAddDto
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public List<OrderItemAddDto> Items { get; set; }
    }
}
