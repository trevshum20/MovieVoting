using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MovieVoting.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Password123456!YareYareDazekujotaro";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDBContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppIdentityDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userMangager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userMangager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "admin@yeet.com";
                user.PhoneNumber = "555-1234";

                await userMangager.CreateAsync(user, adminPassword);
            }
        }
    }
}
