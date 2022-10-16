using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
//using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;

namespace RecruitmentSystemWebApplication.Controllers
{
    /// <summary>
    /// Class <c>ChangePasswordController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required to change a user password.
    /// </summary>

    // The Controller action methods contained in this Controller class require that a user is assigned a role (hence
    // authenticated/signed-in).
    // Reference https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-6.0
    [Authorize]
    public class ChangePasswordController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public ChangePasswordController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Login Manager Reference https://code-maze.com/authentication-aspnet-core-identity/

        /// <summary>
        /// Method <c>ChangePassword</c> is the controller action method of type get, which returns the ChangePasswordModel object
        /// to the ChangePassword view to the user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {

            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
            var User = await _userManager.GetUserAsync(HttpContext.User);
            changePasswordModel.Username = User.UserName;
            return View(changePasswordModel);
        }

        /// <summary>
        /// Method <c>ChangePassword</c> is the controller action method of type post, which attempts to change the user's password after
        /// the user submits the ChangePassword form from the respective view. This controller action method calls the
        /// UserIdentityApplicationLogic class which is responsible of handling the application logic to change a user password and return
        /// the result. Then, if the change password result is successful, the controller action method refreshes the user's SignIn
        /// cookie and returns a feedback message indicating that the operation was successful, or returns feedback message indicating
        /// that the user password change attempt failed.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            // Get the username of the user which submitted the change password from the view using the security information of the
            // current HTTP request.
            // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.web.httpcontext.user?view=netframework-4.8&viewFallbackFrom=net-6.0
            var user = await _userManager.GetUserAsync(HttpContext.User);
            changePasswordModel.Username = user.UserName;

            // If server-side validation on the Model (containing the user inputs and username retrieved from the previous step) passes,
            // attempt to change the user password using the ChangeUserPassword method contained in the UserIdentityApplicationLogic
            // class.
            if (ModelState.IsValid)
            {
                UserIdentityApplicaitonLogic userIdentityApplicationLogicObject = new UserIdentityApplicaitonLogic(_userManager);
                var ChangeUserPasswordAttemptResult = await userIdentityApplicationLogicObject.ChangeUserPassword(user,
                                                                                                                  changePasswordModel.CurrentPassword,
                                                                                                                  changePasswordModel.NewPassword);

                // If the user password change result is successful, refresh the user's SignIn cookie and return a feedback message
                // indicating that the password change was successful to the view.
                if (ChangeUserPasswordAttemptResult.Succeeded)
                {
                    // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1.refreshsigninasync?view=aspnetcore-6.0
                    await _signInManager.RefreshSignInAsync(user);

                    changePasswordModel.PasswordChangeAlertID = 1;
                    return View(changePasswordModel);
                }

                // Else (hence the user password change attempt result indicates a failure, add the error messages from the retrieved
                // IdentityResult to the ModelState and return them to the view along with a feedback message indicating that the user
                // password change failed.
                else
                {
                    foreach (var Error in ChangeUserPasswordAttemptResult.Errors)
                    {
                        ModelState.TryAddModelError(Error.Code, Error.Description);
                    }

                    changePasswordModel.PasswordChangeAlertID = 2;
                    return View(changePasswordModel);
                }
            }

            // Else (hence the server-side validation on the model did not pass), add an error message to the ModelState and return the
            // model to the view. The added error message is also displayed in the view.
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(changePasswordModel);
            }
        }
    }
}

