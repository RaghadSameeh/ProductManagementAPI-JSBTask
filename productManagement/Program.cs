
using DataAccessLayer.Data;
using DataAccessLayer.Reposatries.OrderReposatry;
using DataAccessLayer.Reposatries.ProductReposatry;
using Microsoft.EntityFrameworkCore;

namespace productManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //registerInterfaces
            builder.Services.AddScoped<IProduct,ProductRepo>();
            builder.Services.AddScoped<IOrder, OrderRepo>();


            //registerDB
            builder.Services.AddDbContext<Context>(option =>
            {
                option.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("sql"),
                b => b.MigrationsAssembly("productManagement"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
