using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public interface IWIApiContext
    {
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}