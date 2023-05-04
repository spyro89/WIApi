using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WIApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddScoped
            //builder.Services.AddSingleton
            //builder.Services.AddTransient

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

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