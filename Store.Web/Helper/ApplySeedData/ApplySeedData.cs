using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.contexts;
using Store.Data.Entities.IdentityEntities;
using Store.Repository;



namespace Store.Web.Helper.ApplySeedData
{
    public class ApplySeeding                              //function بتنادي علي SeedDataAsync
    {
        public static async Task ApplySeedDataAsync(WebApplication app)

        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try

                {
                    var context = services.GetRequiredService<StoreDbContext>();

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();          //for AppidentityseedData

                    await context.Database.MigrateAsync();

                    await StoreContextSeed.SeedDataAsync(context, loggerFactory);

                    await AppIdentitySeedData.SeedUserAsync(userManager);

                   
                }

                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
