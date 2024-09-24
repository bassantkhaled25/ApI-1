using Microsoft.EntityFrameworkCore;
using Store.Data.contexts;
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

                    await context.Database.MigrateAsync();

                    await StoreContextSeed.SeedDataAsync(context, loggerFactory);

                   
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
