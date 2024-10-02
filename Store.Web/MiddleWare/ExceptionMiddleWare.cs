using System.Net;
using System.Text.Json;
namespace Store.Web.MiddleWare
{
    public class ExceptionMiddleWare

    {
            private readonly ILogger _logger;
            private readonly IHostEnvironment _environment;
            private readonly RequestDelegate _next;                  //next middleware in pipeline

            public ExceptionMiddleWare(RequestDelegate next, IHostEnvironment environment , ILogger<ExceptionMiddleWare> logger)
            {
               _next = next;
               _environment = environment;
               _logger = logger;
              
                
            }

            public async Task InvokeAsync(HttpContext context)

            {
                try

                {
                    await _next(context);                                                            //if ok (next request)
                }

                catch (Exception ex)

                {
                    _logger.LogError(ex, ex.Message);       

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;       //casting (enum =>int)

                    var response = _environment.IsDevelopment()                                   //if development => show details


                   ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) // if development => with stacktrace(details)

                   : new CustomException((int)HttpStatusCode.InternalServerError);                  //500    //if not development environment (only statuscode)



                                           //شكل ال response (not required)


                     var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                          var json = JsonSerializer.Serialize(response, options);              //serialize (object => string)
                          await context.Response.WriteAsync(json);


}   }       }   } 

