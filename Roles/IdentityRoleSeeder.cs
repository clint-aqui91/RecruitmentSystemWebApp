using Microsoft.AspNetCore.Identity;

namespace RecruitmentSystemWebApplication.Roles
{

    /// <summary>
    /// Class <c>IdentityRoleSeeder</c> contains a method which checks if a role exists in the Identity Database, and seeds/creates the role if it does not exists in the Identity
    /// Database. The functionality offered by this class is called by the application during its startup.
    /// Reference: https://alexcodetuts.com/2019/05/22/how-to-seed-users-and-roles-in-asp-net-core/
    /// </summary>
    public class IdentityRoleSeeder
    {
        // Starts the Identity Role seeding.
        internal static void IdentityRoleSeeding(RoleManager<IdentityRole> roleManager)
        {
            SeedIdentityRoles(roleManager);
        }

        // Checks if an Identity Role exists within the Identity Database, and creates it if it does not exist.
        /// <summary>
        /// Method <c>SeedIdentityRoles</c> checks whether a role exists in the Identity Database and creates it if it does not exist. It uses the RoleManager API's RoleExistsAsync
        /// and CreateAsync methods to check whether a role exists in the Identity Database, respectively.
        /// method to create the role.
        /// Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.rolemanager-1?view=aspnetcore-6.0
        /// Reference: https://alexcodetuts.com/2019/05/22/how-to-seed-users-and-roles-in-asp-net-core/
        /// </summary>
        static void SeedIdentityRoles(RoleManager<IdentityRole> roleManager)
        {
            // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.rolemanager-1.roleexistsasync?view=aspnetcore-6.0#microsoft-aspnetcore-identity-rolemanager-1-roleexistsasync(system-string)
            if (!roleManager.RoleExistsAsync("Jobseeker").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Jobseeker";
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.rolemanager-1.createasync?view=aspnetcore-6.0#microsoft-aspnetcore-identity-rolemanager-1-createasync(-0)
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Recruiter").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Recruiter";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Tester").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tester";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Tester2").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tester2";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Tester3").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tester3";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
