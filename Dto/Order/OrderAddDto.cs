using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Order
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
