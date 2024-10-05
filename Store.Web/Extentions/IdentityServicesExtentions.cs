using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities;
using Store.Data.Entities.IdentityEntities;
using System.Text;

namespace Demo.API.Extentions
{
    public static class IdentityServicesExtentions               //static عشان مش باخد منه object

    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection services )         //
        {
            var builder = services.AddIdentityCore<AppUser>();             //register identity

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddEntityFrameworkStores<IdentityDbContext>();                 //register for user الحاجات الخاصه بيه 

            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
                 
            return services;
        }
    }
}
