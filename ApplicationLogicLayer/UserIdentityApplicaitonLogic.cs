using Microsoft.AspNetCore.Identity;

namespace RecruitmentSystemWebApplication.ApplicationLogicLayer
{
    /// <summary>
    /// Class <c>UserIdentityApplicaitonLogic</c> holds the application logic layer code for the User Identity.
    /// In the majority of this class's usage, it gets called by classes found in the application logic classes which functionalities
    /// which require Identity functions. It uses and uses the ASP.NET Core APIs to manage the users' identity as required.
    /// </summary>
    public class UserIdentityApplicaitonLogic
    {
        UserManager<IdentityUser> _userManager;

        public UserIdentityApplicaitonLogic(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Method <c>CreateUserIdentity</c> holds the application logic code to create a user identity.
        /// It returns the result returned from the Identity API, to the calling method or controller action.
        /// </summary>
        // Reference: https://code-maze.com/user-registration-aspnet-core-identity/
        public async Task<IdentityResult> CreateUserIdentity(string username, string userEmailAddress, string password, string userRole)
        {
            // Initialize a new Identity user and set its UserName & Email Address attributes.
            var user = new IdentityUser { UserName = username, Email = userEmailAddress };

            /// <summary>
            /// Create the user in the Identity Database using the CreateAsync method found in the UserManager Identity API.
            /// CreateAsync method also ensures that the specified Identity Options/requirements are met, including if another user
            /// with a method username exists and that the supplied password matches the password requirements/complexities.
            /// Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1.createasync?view=aspnetcore-6.0
            /// </summary>
            var UserIdentityCreationResult = await _userManager.CreateAsync(user, password);

            // If Identity Result is successful, assign the User Role to the newly created user identity and return the Identity result
            // to the calling method or controller action.
            if (UserIdentityCreationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRole);

                return UserIdentityCreationResult;
            }

            // If Identity Result is unsuccessful, return the Result to the calling method or controller action.
            else
            {
                return UserIdentityCreationResult;
            }

        }

        /// <summary>
        /// Method <c>ChangeUserPassword</c> holds the application logic code to change the password of a user's identity user.
        /// It returns the result returned from the Identity API, to the calling method or controller action method.
        /// The ChangePasswordAsync method found in the UserManager of the ASP.net Core Identity API, also confirms that the supplied
        /// Current Password is correct (to minimize the risk of having a malicious actor with access to an unattended device with a 
        /// signed in session in the web application, and that the new Password meets the complexity requirements.
        /// </summary>
        public async Task<IdentityResult> ChangeUserPassword(IdentityUser user, string currentPassword, string newPassword)
        {
            // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1.changepasswordasync?view=aspnetcore-6.0
            var UserIdentityPasswordChangeResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return UserIdentityPasswordChangeResult;
        }

    }
}
