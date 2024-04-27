using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Identity
{
    public static class ApplicationIdentityDataSeeding
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Hamada Okasha",
                    Email="Hamada@gmail.com",
                    UserName="Hamada",
                    PhoneNumber="01094484384"
                };
            await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
