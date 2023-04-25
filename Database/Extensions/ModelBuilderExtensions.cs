using DB.Entities;
using DB.Enums;
using Microsoft.EntityFrameworkCore;

namespace DB.Extensions
{
    internal static class ModelBuilderExtensions
    {
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 1.99M,
                    Quantity = 10
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 123.50M,
                    Quantity = 100
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id= 1,
                    CustomerEmail = "client1@gmail.com",
                    CreateDate = DateTime.Now,
                    Status = OrderStatus.Created,
                    TotalPrice = 11.98M
                }
            );

            var orderItems = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Id  = 1,
                            OrderId = 1,
                            Price = 0.99M,
                            ProductId = 1,
                            Quantity = 2,
                            TotalPrice = 1.98M
                        },
                        new OrderItem()
                        {
                            Id = 2,
                            OrderId = 1,
                            Price = 2,
                            ProductId = 2,
                            Quantity = 5,
                            TotalPrice = 10
                        }
                    };
            modelBuilder.Entity<OrderItem>().HasData(orderItems);
        }
    }
}