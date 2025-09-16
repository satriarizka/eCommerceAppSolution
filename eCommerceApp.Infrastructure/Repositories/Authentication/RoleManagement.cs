using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRole(AppUser user, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var result = await userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}
