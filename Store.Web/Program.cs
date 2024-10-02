using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StackExchange.Redis;
using Store.Data.contexts;
using Store.Service.HandleResponses;
using Store.Service.services.product;
using Store.Service.services.product.Dtos;
using Store.Web.Extentions;
using Store.Web.Helper.ApplySeedData;
using Store.Web.MiddleWare;

namespace Store.Web
{  
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreDbContext>(options =>             //register ConnectionString

            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

                                                                                    //Singlton : create one object Shared on the Application

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>              // register connection of redis
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));   //key(redis)
                return ConnectionMultiplexer.Connect(configuration);
            });


            builder.Services.AddApplicationServices();                                  //register services


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await ApplySeeding.ApplySeedDataAsync(app);                           //function بنادي عليها

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleWare>();                         //  قبل ما يروح ع اي حاجه => using middleware <ExceptionMiddleWare>

            app.UseStaticFiles();                                    //for PicURL   //قبل ما اجيب ال route عشان تقرا ال => resource بتاعي

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
