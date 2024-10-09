using Microsoft.OpenApi.Models;

namespace Demo.API.Extentions
{
    public static class AddSwaggerExtention                                      //register at program
    {
        public static IServiceCollection AddSwaggerserviceExtention(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>

            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreAPI", Version = "v1"});

                var SecuritySchema = new OpenApiSecurityScheme

                {
                    Description = "Please enter token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference

                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme,

                    }
                };

                options.AddSecurityDefinition("bearer", SecuritySchema);

                var securityReqirements = new OpenApiSecurityRequirement

                {
                    {SecuritySchema, new [] {"bearer"} }
                };

                options.AddSecurityRequirement(securityReqirements);    

            });

            return services;
        }
    }
}
