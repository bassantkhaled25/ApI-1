using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class AppIdentitySeedData
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser

                {
                    DisplayName = "bassant khaled",
                    Email = "bassantkhaled@gmail.com",
                    UserName = "bassantkhaled",
                    Address = new Address
                    {
                        FirstName = "bassant",
                        LastName = "khaled",
                        Street = "street16",
                        State = "mandara",
                        City = "alex",
                        ZipCode = "1234"
                    }
                };

                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
