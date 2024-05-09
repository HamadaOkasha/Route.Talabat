using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities.Identity;
using System.Security.Claims;

namespace Route.Talabat.APIs.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser?> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await userManager.Users.Include(x => x.Adress).FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
