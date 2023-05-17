using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
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
    }
}