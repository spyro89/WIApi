using BusinessLogic.Repositories.Interfaces;
using DB.Entities;
using DB.Enums;

namespace BusinessLogic.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static ICollection<Order> db = new List<Order>() {
            new Order()
            {
                Id= 1,
                CustomerEmail = "client1@gmail.com",
                CreateDate = DateTime.Now,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Id  =1,
                        Price = 0.99M,
                        ProductId = 1,
                        Quantity = 2,
                        TotalPrice = 1.98M
                    },
                    new OrderItem()
                    {
                        Id  =2,
                        Price = 2,
                        ProductId = 2,
                        Quantity = 5,
                        TotalPrice = 10
                    }
                },
                Status = OrderStatus.Created,
                TotalPrice = 11.98M
            }
        };

        public async Task<Order> GetOneAsync(int id)
        {
            return db.Where(x => x.Id == id).SingleOrDefault();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return db.ToList();
        }

        public async Task AddAsync(Order order)
        {
            order.Id = db.Max(x => (int?)x.Id).GetValueOrDefault(0) + 1;
            db.Add(order);
        }

        public async Task UpdateAsync(Order order)
        {

        }

        public async Task<bool> ExistsAnyActiveOrderWithSelectedProductAsync(int productId)
        {
            return db.Where(x => x.Status == OrderStatus.Created && x.Items.Where(x => x.ProductId == productId).Any())
                .Any();
        }
    }
}
