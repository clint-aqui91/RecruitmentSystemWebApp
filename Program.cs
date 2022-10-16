using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using RecruitmentSystemWebApplication.Data;
using RecruitmentSystemWebApplication.Roles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Identity Database Connection String.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var IdentityDBConnectionString = builder.Configuration.GetConnectionString("IdentityDBConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(IdentityDBConnectionString));

// Add Web App Data Datbase Connection String.
var WebAppDataDBConnectionString = builder.Configuration.GetConnectionString("WebAppDataDBConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(WebAppDataDBConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ASP.NET Core Identity configuration for the project.
// Reference: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-6.0
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    // User Settings
    // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.useroptions?view=aspnetcore-6.0
    options.User.RequireUniqueEmail = true;

    // Lockout Settings.
    // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.lockoutoptions?view=aspnetcore-6.0
    // Note: Sign-In Settings are disabled, since this is a development environment.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(0);
    options.Lockout.MaxFailedAccessAttempts = 0;
    options.Lockout.AllowedForNewUsers = false;

    // User Password settings.
    // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.passwordoptions?view=aspnetcore-6.0
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 16;
    options.Password.RequiredUniqueChars = 2;

    // Sign-In Settings
    // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinoptions?view=aspnetcore-6.0
    // Note: Sign-In Settings are disabled, since this is a development environment.
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddRoles<IdentityRole>()
    //.AddRoleManager<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Cookie Settings
// Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.cookies.cookieauthenticationoptions?view=aspnetcore-6.0
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    //options.AccessDeniedPath = "/Login/Login";
    options.Cookie.Name = "RecruitmentSystemWebCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
//builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// Check if Roles exist in Identity Database and create them if they do not exist
using (var scope = app.Services.CreateScope())
{

    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //Seeder.SeedData(roleManager);
    IdentityRoleSeeder.IdentityRoleSeeding(roleManager);
}

app.Run();

// Class containing the methods to check if roles exist within Identity Database and create them if they do not exist.
/*public class Seeder
{

    internal static void SeedData(RoleManager<IdentityRole> roleManager)
    {
        //SeedIdentityRoles(roleManager);
        Console.WriteLine("I am in first class method SeedData");
    }
    static void SeedIdentityRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Jobseeker").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Jobseeker";
            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }


        if (!roleManager.RoleExistsAsync("Recruiter").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Recruiter";
            IdentityResult roleResult = roleManager.
            CreateAsync(role).Result;
        }


    }
}*/





