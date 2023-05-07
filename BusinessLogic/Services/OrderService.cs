
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

        public OrderAddResult Add(OrderAddDto order)
        {
            var products = productRepository.GetAllByIds(order.Items.Select(x => x.ProductId).ToList());

            bool allProductsExistValidationResult = CheckOrderProductExistance(order.Items, products);
            if (!allProductsExistValidationResult)
            {
                return OrderAddResult.ProductDoesNotExist;
            }

            bool productQuantityValidationResult = CheckOrderProductQuantity(order.Items, products);
            if (!productQuantityValidationResult)
            {
                return OrderAddResult.ProductQuantityValidationFailed;
            }

            var newOrder = CreateNewOrder(order, products);
            orderRepository.Add(newOrder);
            ModifyOrderProductsQuantity(newOrder.Items, newOrder.Status, products);
            return OrderAddResult.Ok;
        }

        public OrderChangeStatusResult ChangeStatus(int id, OrderChangeStatusDto orderChangeStatus)
        {
            var order = orderRepository.GetOne(id);
            if (order == null)
            {
                return OrderChangeStatusResult.OrderNotFound;
            }

            var canChangeStatusResult = order.CanChangeOrderStatus(order.Status, orderChangeStatus.Status);
            if (!canChangeStatusResult)
            {
                return OrderChangeStatusResult.StatusChangeNotAllowed;
            }

            order.Status = orderChangeStatus.Status;
            orderRepository.Update(order);

            var products = productRepository.GetAllByIds(order.Items.Select(x => x.ProductId).ToList());
            ModifyOrderProductsQuantity(order.Items, orderChangeStatus.Status, products);

            return OrderChangeStatusResult.Ok;
        }

        public List<OrderDto> GetList()
        {
            var orders = orderRepository.GetAll();
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

        private bool CheckOrderProductExistance(List<OrderItemAddDto> orderItems, List<Product> products)
        {
            foreach (var orderItem in orderItems)
            {
                var product = products.Where(x => x.Id == orderItem.ProductId).SingleOrDefault();
                if (product == null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckOrderProductQuantity(List<OrderItemAddDto> orderItems, List<Product> products)
        {
            foreach (var orderItem in orderItems)
            {
                var product = products.Where(x => x.Id == orderItem.ProductId).Single();
                if (product.Quantity - orderItem.Quantity < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ModifyOrderProductsQuantity(ICollection<OrderItem> orderItems, OrderStatus orderStatus, List<Product> products)
        {
            if (orderStatus == OrderStatus.Created || orderStatus == OrderStatus.Canceled)
            {
                foreach (var orderItem in orderItems)
                {
                    var product = products.Where(x => x.Id == orderItem.ProductId).Single();
                    if (orderStatus == OrderStatus.Canceled)
                    {
                        product.Quantity += orderItem.Quantity;
                    }
                    else
                    {
                        product.Quantity -= orderItem.Quantity;
                    }
                    productRepository.Update(product);
                }
            }
        }

        private decimal GetTotalPrice(List<OrderItemAddDto> orderItems, List<Product> products)
        {
            var result = 0M;
            foreach (var orderItem in orderItems)
            {
                var product = products.Where(x => x.Id == orderItem.ProductId).Single();
                result += product.Price * product.Quantity;
            }
            return result;
        }

        private Order CreateNewOrder(OrderAddDto order, List<Product> products)
        {
            decimal totalPrice = GetTotalPrice(order.Items, products);

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

            return newOrder;
        }
    }
}
