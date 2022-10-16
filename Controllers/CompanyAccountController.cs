using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
//using System.Diagnostics;
//using RecruitmentSystemWebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;

namespace RecruitmentSystemWebApplication.Controllers

{

    /// <summary>
    /// Class <c>CompanyAccountController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required for the Company Account Management functionality.
    /// </summary>

    // Only signed-in users with the "Recruiter" role can make use of the controller action methods contained in this controller class.
    [Authorize(Roles = "Recruiter")]
    public class CompanyAccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CompanyAccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Method <c>CompanyAccountMainMenu</c> returns the Company Account Main Menu View to a recruiter, to manage his account.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CompanyAccountMainMenu()
        {
            return View();
        }

        /// <summary>
        /// Http Get Method <c>UpdateCompany</c> returns the Update Company View along with the model.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> UpdateCompany()
        {
            CompanyModel companyModel = new CompanyModel();
            
            // Get the current username.
            var user = await _userManager.GetUserAsync(HttpContext.User);
            companyModel.RecruiterSignInEmailAddress = user.UserName;

            // Populate the required Dropdown Menus / Select Lists for the form in the respective view
            Town town = new Town();
            companyModel.townList = town.GetTownList();

            CompanySize companySize = new CompanySize();
            companyModel.companySizeList = companySize.GetCompanySizeList();

            Industry industry = new Industry();
            companyModel.industryList = industry.GetIndustryList();

            // Call the application logic method which retrieves the company details/record by recruiter username.
            CompanyApplicationLogic companyApplicationLogicObject = new CompanyApplicationLogic(_userManager);
            companyModel = companyApplicationLogicObject.GetCompanyDetailsByRecruiterUsername(companyModel);
            //var state1 = ModelState;

            // Removes validation (in this case client-side validation) for the Company Model when used in the Update Company context.
            ModelState.Remove("RecruiterSignInEmailAddress");
            ModelState.Remove("RecruiterSignInPassword");
            ModelState.Remove("ConfirmPassword");
            
            // ModelState.Remove on the IFormFile does not work.
            ModelState.Remove(nameof(companyModel.CompanyLogoFile));
            //var keys = ModelState.Keys;
            //var state2 = ModelState;

            // Return the UpdateCompanyView with the CompanyModel containing the company details.
            return View(companyModel);
        }

        /// <summary>
        /// Http Post Method <c>UpdateCompany</c> handles the UI logic when the user submits the form in the UpdateCompany View.
        /// It uses the CompanyApplicationLogic to update the company details.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateCompany(CompanyModel companyModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            companyModel.RecruiterSignInEmailAddress = user.UserName;

            // Remove server side validation for the below Model attributes.
            ModelState.Remove("RecruiterSignInEmailAddress");
            ModelState.Remove("RecruiterSignInPassword");
            ModelState.Remove("ConfirmPassword");
            //ModelState.Remove("CompanyLogoFile");

            // ModelState.Remove does not work on IFormFile Model attributes.
            ModelState.Remove(nameof(companyModel.CompanyLogoFile));

            // If server-side validation on the Model containing the inputted values posted from the form passes, call the respective
            // Application Logic method responsible of updating the Company details.
            if (ModelState.IsValid)
            {
                CompanyApplicationLogic companyApplicationLogicObject = new CompanyApplicationLogic(_userManager);
                companyModel = companyApplicationLogicObject.UpdateCompanyLogic(companyModel);

                // Get the respective lists for the respective drop-down menus for the view and return the view.
                Town town = new Town();
                companyModel.townList = town.GetTownList();

                CompanySize companySize = new CompanySize();
                companyModel.companySizeList = companySize.GetCompanySizeList();

                Industry industry = new Industry();
                companyModel.industryList = industry.GetIndustryList();

                return View(companyModel);
            }

            // Else (hence the server-side validation on the model object containing the submitted values from the form did not pass),
            // populate the required lists for the View Drop-down / select lists, return the model with the view and display an error
            // message to the user.
            else
            {
                companyModel.CompanyUpdateAlertID = 2;

                Town town = new Town();
                companyModel.townList = town.GetTownList();

                CompanySize companySize = new CompanySize();
                companyModel.companySizeList = companySize.GetCompanySizeList();

                Industry industry = new Industry();
                companyModel.industryList = industry.GetIndustryList();

                return View(companyModel);
            }
        }
    }
}
