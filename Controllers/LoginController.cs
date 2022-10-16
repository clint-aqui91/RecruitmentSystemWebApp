using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
using System.Diagnostics;

namespace RecruitmentSystemWebApplication.Controllers
{
    /// <summary>
    /// Class <c>LoginController</c> is the controller class containing the controller action methods which handle Login and Logout
    /// functionalities.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Login Manager Reference https://code-maze.com/authentication-aspnet-core-identity/

        /// <summary>
        /// Controller HTTP Get Action Method <c>Login</c> returns the login view populated with the login model.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LogInModel logInModel = new LogInModel();
            return View(logInModel);
        }

        /// <summary>
        /// Controller HTTP Post Action Method <c>Login</c> controls the logic to sign-in the user using the SignInManager Identity API
        /// and redirects to the appropriate controller (either CompanyAccountController or JobseekerAccountController), depending on the
        /// signed/logged in user's identity role.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LogInModel logInModel)
        {
            // PasswordSignInAsync requires username, password & two boolean values for isPersistent (Remember Me) and flag indicating whether user account should be locked if an invalid login attempt is performed.
            // Reference https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1.passwordsigninasync?view=aspnetcore-6.0
            var SignInResult = await _signInManager.PasswordSignInAsync(logInModel.Username, logInModel.Password, isPersistent: false, lockoutOnFailure: false);

            // If sign-in was successful, get the roles for the signed-in user.
            if (SignInResult.Succeeded)
            {
                // Roles & Authorization Reference - https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-6.0

                var user = await _userManager.FindByNameAsync(logInModel.Username);
                var role = await _userManager.GetRolesAsync(user);

                // If user is assigned the Recruiter role, redirect to the CompanyAccountController
                if (await _userManager.IsInRoleAsync(user, "Recruiter"))
                {
                    //RedirectToAction structure Controller.Action, Folder containing view
                    return RedirectToAction(nameof(CompanyAccountController.CompanyAccountMainMenu), "CompanyAccount");
                }

                // Else if user is assigned the jobseeker role, redirect to the JobseekerAccountController
                else if (await _userManager.IsInRoleAsync(user, "Jobseeker"))
                {
                    return RedirectToAction(nameof(JobseekerAccountController.JobseekerAccountMainMenu), "JobseekerAccount");
                }

                // Else (sign-in was successful, however user's role is neither that of a Recruiter nor that of a Jobseeker, add a Model
                // error to be displayed in the view and return the Login View
                else
                {
                    ModelState.AddModelError("", "Something went wrong - Role not recognised.");
                    return View();
                }
            }

            // Else (sign-in was not successful) add an error to the model to be displayed to the user through the Login view.
            else
            {
                // As a security best practice, sign-in errors attributed to credentials do not indicate if user account exists, and which
                // credential is incorrect (password or username).
                ModelState.AddModelError("", "Invalid Credentials");

                return View();
            }
        }

        /// <summary>
        /// Controller Action Method <c>LogOut</c> controls the logic to sign-out/logout a user by using the method SignOutAsync method
        /// found in the SignInManager Identity API and redirects to the Home controller.
        /// </summary>
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // Not used - Created automatically together with the controller (since a controller template was used for this controller class).
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
