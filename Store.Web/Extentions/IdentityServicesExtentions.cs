using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities;
using Store.Data.Entities.IdentityEntities;
using System.Text;

namespace Demo.API.Extentions
{
    public static class IdentityServicesExtentions               //static عشان مش باخد منه object

    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services , IConfiguration _config )         //
        {
            var builder = services.AddIdentityCore<AppUser>();                           //register identity

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddEntityFrameworkStores<IdentityDbContext>();                        //register for user الحاجات الخاصه بيه 

            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters

                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"])),
                            ValidateIssuer = true,
                            ValidIssuer = _config["Token:Issuer"],       
                            ValidateAudience = false
                        };
                    });

            return services;
                 
          
        }
    }
}
