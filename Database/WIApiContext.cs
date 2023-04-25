using DB.Entities;
using DB.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class WIApiContext : DbContext, IWIApiContext
    {
        public WIApiContext()
        {
            
        }
        public WIApiContext(DbContextOptions<WIApiContext> options) : base(options)
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
                .HasKey(x => x.Id);

            modelBuilder.Entity<Product>()
                .Property(x => x.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .IsRequired()
                .HasPrecision(13, 2);



            modelBuilder.Entity<Order>()
                .ToTable("Order");

            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Order>()
                .Property(x => x.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<Order>()
                .Property(x => x.CustomerEmail)
                .IsRequired()
                .HasMaxLength(50);


            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem");

            modelBuilder.Entity<OrderItem>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<OrderItem>()
                .Property(x => x.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

            modelBuilder.Entity<OrderItem>()
                .Property(x => x.Price)
                .IsRequired()
                .HasPrecision(13, 2);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Seed();
        }
    }
}