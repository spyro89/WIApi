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
        public ActionResult<IEnumerable<OrderDto>> GetList()
        {
            return Ok(orderService.GetList());
        }

        [HttpPost]
        public ActionResult Add([FromBody] OrderAddDto order)
        {
            var result = orderService.Add(order);
            return result switch
            {
                OrderAddResult.Ok => NoContent(),
                OrderAddResult.ProductQuantityValidationFailed => StatusCode(StatusCodes.Status405MethodNotAllowed, "Insufficient product quantity"),
                OrderAddResult.ProductDoesNotExist => NotFound("At least one product not found")
            };
        }

        [HttpPatch("{id}")]
        public ActionResult ChangeStatus([FromRoute] int id, [FromBody] OrderChangeStatusDto orderChangeStatus)
        {
            var result = orderService.ChangeStatus(id, orderChangeStatus);
            return result switch
            {
                OrderChangeStatusResult.Ok => NoContent(),
                OrderChangeStatusResult.OrderNotFound => NotFound(),
                OrderChangeStatusResult.StatusChangeNotAllowed => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }
    }
}