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

            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WIApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DB.xml"));
            });

            // Add services to the container.
            builder.Services.AddScoped<IProductRepository, DbProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, DbOrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddDbContext<WIApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Orders"));
            });

            var context = builder.Services.BuildServiceProvider().GetService<WIApiContext>();
            context.Database.Migrate();

            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "WIApi";
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}