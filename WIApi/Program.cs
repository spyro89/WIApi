using BusinessLogic.Repositories.DbImplementations;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB;
using Microsoft.EntityFrameworkCore;

namespace WIApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IProductRepository, DbProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, DbOrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddDbContext<WIApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Orders"));
            });

            builder.Services.AddScoped<IWIApiContext, WIApiContext>();

            var context = builder.Services.BuildServiceProvider().GetService<WIApiContext>();
            context.Database.Migrate();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}