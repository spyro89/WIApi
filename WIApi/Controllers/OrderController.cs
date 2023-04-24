//using BusinessLogic.Dto.Order;
//using BusinessLogic.Enums;
//using BusinessLogic.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace WIApi.Controllers
//{
//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class OrderController : ControllerBase
//    {
//        private IOrderService orderService;

//        public OrderController(X)
//        {
//            X
//        }

//        X
//        public async Task<ActionResult<IEnumerable<OrderDto>>> GetList()
//        {
//            return X(await orderService.GetListAsync());
//        }

//        X
//        public async Task<ActionResult> Add(X OrderAddDto order)
//        {
//            var result = await orderService.AddAsync(order);
//            return result switch
//            {
//                OrderAddResult.Ok => X,
//                OrderAddResult.ProductQuantityValidationFailed => StatusCode(StatusCodes.Status405MethodNotAllowed, "Insufficient product quantity"),
//                OrderAddResult.ProductDoesNotExist => X("At least one product not found")
//            };
//        }

//        X
//        public async Task<ActionResult> ChangeStatus(X int id, X OrderChangeStatusDto orderChangeStatus)
//        {
//            var result = await orderService.ChangeStatusAsync(id, orderChangeStatus);
//            return result switch
//            {
//                OrderChangeStatusResult.Ok => X,
//                OrderChangeStatusResult.OrderNotFound => X,
//                OrderChangeStatusResult.StatusChangeNotAllowed => StatusCode(StatusCodes.Status405MethodNotAllowed)
//            };
//        }
//    }
//}