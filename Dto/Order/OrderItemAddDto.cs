using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Order
{
    public class OrderItemAddDto
    {
        public int ProductId { get; set; }
        [Range(1, short.MaxValue)]
        public short Quantity { get; set; }
    }
}
