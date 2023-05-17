using DB.Entities;
using DB.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class WIApiContext : DbContext
{
    public WIApiContext(DbContextOptions<WIApiContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .ToTable("Product");
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .UseIdentityColumn(seed: 1, increment: 1);

        modelBuilder.Entity<Order>()
            .ToTable("Order");
        modelBuilder.Entity<Order>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Order>()
            .Property(p => p.Id)
            .UseIdentityColumn(seed: 1, increment: 1);

        modelBuilder.Entity<OrderItem>()
            .ToTable("OrderItem");
        modelBuilder.Entity<OrderItem>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<OrderItem>()
            .Property(p => p.Id)
            .UseIdentityColumn(seed: 1, increment: 1);

        modelBuilder.Entity<OrderItem>()
            .HasOne(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId);

        modelBuilder.Seed();
    }
}