using BusinessLogic.Dto.Order;
using BusinessLogic.Enums;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WIApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetList()
        {
            return Ok(await orderService.GetListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] OrderAddDto order)
        {
            var result = await orderService.AddAsync(order);
            return result switch
            {
                OrderAddResult.Ok => NoContent(),
                OrderAddResult.ProductQuantityValidationFailed => StatusCode(StatusCodes.Status405MethodNotAllowed, "Insufficient product quantity"),
                OrderAddResult.ProductDoesNotExist => NotFound("At least one product not found")
            };
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> ChangeStatus([FromRoute] int id, [FromBody] OrderChangeStatusDto orderChangeStatus)
        {
            var result = await orderService.ChangeStatusAsync(id, orderChangeStatus);
            return result switch
            {
                OrderChangeStatusResult.Ok => NoContent(),
                OrderChangeStatusResult.OrderNotFound => NotFound(),
                OrderChangeStatusResult.StatusChangeNotAllowed => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }
    }
}