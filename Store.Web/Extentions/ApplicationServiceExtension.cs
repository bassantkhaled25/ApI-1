using Microsoft.AspNetCore.Mvc;
using Store.Service.services.product.Dtos;
using Store.Service.services.product;
using Store.Service.HandleResponses;
using store.Services.CacheServices;
using Store.Service;
using Store.Repository.Basket;
using Store.Service.BasketService;
using Services.BasketServices;

namespace Store.Web.Extentions
{
    public static class ApplicationServiceExtension

    {                                                                              //instead of program
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)   //extension method

        {

            Services.AddScoped<IUnitOfWork, UnitOfWork>();                            

            Services.AddScoped<IProductService, ProductService>(); 
            
            Services.AddScoped<IBasketRepository, BasketRepository>();

            Services.AddScoped<IBasketServices, BasketServices>();

            Services.AddScoped<ICasheService, CacheService>();                          

            Services.AddAutoMapper(typeof(ProductProfile)); 
            
            Services.AddAutoMapper(typeof(BasketProfile));  

         
            Services.Configure<ApiBehaviorOptions>(options =>

            {
                options.InvalidModelStateResponseFactory = actionContext =>

                {
                    var errors = actionContext.ModelState                              //query in modelstate 

                    .Where(model => model.Value?.Errors.Count > 0)                      //check لو فيه errors or not

                    .SelectMany(model => model.Value?.Errors)                        // (الليست الكبيره اللي بره)every prop could have errors (name -age - email .....) lists of errors

                    .Select(error => error.ErrorMessage)                              //هدخل جوه كل ليست من ال prop

                    .ToList();


                    var ErrorResponse = new ValidationErrorResponse

                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ErrorResponse);

                };

            });

            return Services;    


        }
    }
}
