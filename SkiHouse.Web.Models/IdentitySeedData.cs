using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SkiHouse.Web.Data
{
    public class IdentitySeedData
    {
        public const string ADMIN_USER = "Admin";
        public const string ADMIN_PWD = "1qaz!QAZ";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                IdentityUser user = await userManager.FindByIdAsync(ADMIN_USER);

                if (user == null)
                {
                    user = new IdentityUser(ADMIN_USER);

                    await userManager.CreateAsync(user, ADMIN_PWD);
                }
            }
        }
    }
}
