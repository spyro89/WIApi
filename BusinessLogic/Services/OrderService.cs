
using BusinessLogic.Dto.Order;
using BusinessLogic.Enums;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using DB.Enums;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public async Task<OrderAddResult> AddAsync(OrderAddDto order)
        {
            bool produtQuantityValidationResult = await CheckOrderProductQuantityAsync(order.Items);
            if (!produtQuantityValidationResult)
            {
                return OrderAddResult.ProductQuantityValidationFailed;
            }

            var products = await productRepository.GetAllByIdsAsync(order.Items.Select(x => x.ProductId).ToList());
            decimal totalPrice = 0;
            foreach (var orderItem in order.Items)
            {
                var product = products.Where(x => x.Id == orderItem.ProductId).SingleOrDefault();
                if(product == null)
                {
                    return OrderAddResult.ProductDoesNotExist;
                }
                totalPrice += product.Price * product.Quantity;
            }

            var newOrder = new Order()
            {
                CustomerEmail = order.CustomerEmail,
                Status = OrderStatus.Created,
                CreateDate = DateTime.Now,
                Items = new List<OrderItem>(),
                TotalPrice = totalPrice
            };

            foreach (var orderItem in order.Items)
            {
                var product = products.Where(x => x.Id == orderItem.ProductId).Single();

                newOrder.Items.Add(new OrderItem()
                {
                    Price = product.Price,
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    TotalPrice = product.Price * orderItem.Quantity
                });
            }
            await orderRepository.AddAsync(newOrder);
            await ModifyOrderProductsQuantityAsync(newOrder.Items, newOrder.Status);
            return OrderAddResult.Ok;
        }

        public async Task<OrderChangeStatusResult> ChangeStatusAsync(int id, OrderChangeStatusDto orderChangeStatus)
        {
            var order = await orderRepository.GetOneAsync(id);
            if (order == null)
            {
                return OrderChangeStatusResult.OrderNotFound;
            }

            var canChangeStatusResult = CanChangeOrderStatus(order.Status, orderChangeStatus.Status);
            if (!canChangeStatusResult)
            {
                return OrderChangeStatusResult.StatusChangeNotAllowed;
            }

            order.Status = orderChangeStatus.Status;
            await orderRepository.UpdateAsync(order);

            await ModifyOrderProductsQuantityAsync(order.Items, orderChangeStatus.Status);

            return OrderChangeStatusResult.Ok;
        }

        public async Task<IEnumerable<OrderDto>> GetListAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            var result = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderResult = new OrderDto()
                {
                    Id = order.Id,
                    CustomerEmail = order.CustomerEmail,
                    Date = order.CreateDate,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    Items = new List<OrderItemDto>()
                };

                foreach (var orderItem in order.Items)
                {
                    orderResult.Items.Add(new OrderItemDto()
                    {
                        Id = orderItem.Id,
                        Price = orderItem.Price,
                        ProductId = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        TotalPrice = orderItem.TotalPrice
                    });
                }
                result.Add(orderResult);
            }
            return result;
        }

        private async Task<bool> CheckOrderProductQuantityAsync(IEnumerable<OrderItemAddDto> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var product = await productRepository.GetOneAsync(orderItem.ProductId);
                if (product.Quantity - orderItem.Quantity < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task ModifyOrderProductsQuantityAsync(ICollection<OrderItem> orderItems, OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.Created || orderStatus == OrderStatus.Canceled)
            {
                foreach (var orderItem in orderItems)
                {
                    var product = await productRepository.GetOneAsync(orderItem.ProductId);
                    if (orderStatus == OrderStatus.Canceled)
                    {
                        product.Quantity += orderItem.Quantity;
                    }
                    else
                    {
                        product.Quantity -= orderItem.Quantity;
                    }
                    await productRepository.UpdateAsync(product);
                }
            }
        }

        private bool CanChangeOrderStatus(OrderStatus currentStatus, OrderStatus newStatus)
        {
            if(currentStatus == OrderStatus.Created)
            {
                return newStatus == OrderStatus.Canceled || newStatus == OrderStatus.Finished;
            }
            return false;
        }
    }
}
