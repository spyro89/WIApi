using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dto.Order
{
    public class OrderItemAddDto
    {
        public int ProductId { get; set; }
        [Range(1, short.MaxValue)]
        public short Quantity { get; set; }
    }
}
