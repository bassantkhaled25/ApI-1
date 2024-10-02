using Microsoft.AspNetCore.Mvc;
using Store.Service.services.product.Dtos;
using Store.Service.services.product;
using Store.Service.HandleResponses;
using store.Services.CacheServices;
using Store.Service;

namespace Store.Web.Extentions
{
    public static class ApplicationServiceExtension

    {                                                                              //instead of program
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)   //extension method

        {

            Services.AddScoped<IUnitOfWork, UnitOfWork>();                            //register IUnitOfWork

            Services.AddScoped<IProductService, ProductService>();                     //register productservice

            Services.AddScoped<ICasheService, CacheService>();                            //Icashservise

            Services.AddAutoMapper(typeof(ProductProfile));                   // register automapper (class productprofile))

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
