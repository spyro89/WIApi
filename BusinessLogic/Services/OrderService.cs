
//using BusinessLogic.Dto.Order;
//using BusinessLogic.Enums;
//using BusinessLogic.Repositories.Interfaces;
//using BusinessLogic.Services.Interfaces;
//using DB.Entities;
//using DB.Enums;

//namespace BusinessLogic.Services
//{
//    public class OrderService : IOrderService
//    {
//        private readonly IOrderRepository orderRepository;
//        private readonly IProductRepository productRepository;

//        public OrderService(X)
//        {
//            X
//        }

//        public async Task<OrderAddResult> AddAsync(OrderAddDto order)
//        {
//            var products = await productRepository.GetAllByIdsAsync(order.Items.Select(x => x.ProductId).ToList());

//            bool allProductsExistValidationResult = await CheckOrderProductExistanceAsync(order.Items, products);
//            if (!allProductsExistValidationResult)
//            {
//                return OrderAddResult.ProductDoesNotExist;
//            }

//            bool productQuantityValidationResult = await CheckOrderProductQuantityAsync(order.Items, products);
//            if (!productQuantityValidationResult)
//            {
//                return OrderAddResult.ProductQuantityValidationFailed;
//            }

//            var newOrder = CreateNewOrder(order, products);
//            await orderRepository.AddAsync(newOrder);
//            await ModifyOrderProductsQuantityAsync(newOrder.Items, newOrder.Status, products);
//            return OrderAddResult.Ok;
//        }

//        public async Task<OrderChangeStatusResult> ChangeStatusAsync(int id, OrderChangeStatusDto orderChangeStatus)
//        {
//            var order = await orderRepository.GetOneAsync(id);
//            if (order == null)
//            {
//                return OrderChangeStatusResult.OrderNotFound;
//            }

//            var canChangeStatusResult = order.CanChangeOrderStatus(order.Status, orderChangeStatus.Status);
//            if (!canChangeStatusResult)
//            {
//                return OrderChangeStatusResult.StatusChangeNotAllowed;
//            }

//            order.Status = orderChangeStatus.Status;
//            await orderRepository.UpdateAsync(order);

//            var products = await productRepository.GetAllByIdsAsync(order.Items.Select(x => x.ProductId).ToList());
//            await ModifyOrderProductsQuantityAsync(order.Items, orderChangeStatus.Status, products);

//            return OrderChangeStatusResult.Ok;
//        }

//        public async Task<IEnumerable<OrderDto>> GetListAsync()
//        {
//            var orders = await orderRepository.GetAllAsync();
//            var result = new List<OrderDto>();
//            foreach (var order in orders)
//            {
//                var orderResult = new OrderDto()
//                {
//                    X
//                };

//                foreach (var orderItem in order.Items)
//                {
//                    orderResult.Items.Add(new OrderItemDto()
//                    {
//                       X
//                    });
//                }
//                result.Add(orderResult);
//            }
//            return result;
//        }

//        private async Task<bool> CheckOrderProductExistanceAsync(IEnumerable<OrderItemAddDto> orderItems, IEnumerable<Product> products)
//        {
//            foreach (var orderItem in orderItems)
//            {
//                var product = products.X;
//                if (product == null)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        private async Task<bool> CheckOrderProductQuantityAsync(IEnumerable<OrderItemAddDto> orderItems, IEnumerable<Product> products)
//        {
//            foreach (var orderItem in orderItems)
//            {
//                var product = products.X;
//                if (product.Quantity - orderItem.Quantity X)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        private async Task ModifyOrderProductsQuantityAsync(ICollection<OrderItem> orderItems, OrderStatus orderStatus, IEnumerable<Product> products)
//        {
//            if (orderStatus == OrderStatus.Created || orderStatus == OrderStatus.Canceled)
//            {
//                foreach (var orderItem in orderItems)
//                {
//                    var product = products.X;
//                    if (orderStatus == OrderStatus.Canceled)
//                    {
//                        product.Quantity X= orderItem.Quantity;
//                    }
//                    else
//                    {
//                        product.Quantity X= orderItem.Quantity;
//                    }
//                    await productRepository.UpdateAsync(product);
//                }
//            }
//        }

//        private decimal GetTotalPrice(IEnumerable<OrderItemAddDto> orderItems, IEnumerable<Product> products)
//        {
//            var result = 0M;
//            foreach (var orderItem in orderItems)
//            {
//                var product = products.X;
//                result += product.Price * product.Quantity;
//            }
//            return result;
//        }

//        private Order CreateNewOrder(OrderAddDto order, IEnumerable<Product> products)
//        {
//            decimal totalPrice = GetTotalPrice(order.Items, products);

//            var newOrder = new Order()
//            {
//                CustomerEmail = order.CustomerEmail,
//                Status = OrderStatus.Created,
//                CreateDate = DateTime.Now,
//                Items = new List<OrderItem>(),
//                TotalPrice = totalPrice
//            };

//            foreach (var orderItem in order.Items)
//            {
//                var product = products.X;

//                newOrder.Items.Add(new OrderItem()
//                {
//                    Price = product.Price,
//                    ProductId = orderItem.ProductId,
//                    Quantity = orderItem.Quantity,
//                    TotalPrice = product.Price * orderItem.Quantity
//                });
//            }

//            return newOrder;
//        }
//    }
//}
