using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;
//using System.Data;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;
using Microsoft.AspNetCore.Identity;


namespace RecruitmentSystemWebApplication.Controllers
{

    /// <summary>
    /// Class <c>RegisterCompanyController</c> contains the controller action methods which handle the UI logic to register a company.
    /// </summary>
    public class RegisterCompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterCompanyController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Controller HTTP Get action method <c>RegisterCompany</c> returns the RegisterCompany View populated with the company model.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> RegisterCompany()
        {
            CompanyModel companyModel = new CompanyModel();

            // Get the list for the RegisterCompany View's dropdown menus.
            Town town = new Town();
            companyModel.townList = town.GetTownList();

            CompanySize companySize = new CompanySize();
            companyModel.companySizeList = companySize.GetCompanySizeList();

            Industry industry = new Industry();
            companyModel.industryList = industry.GetIndustryList();

            // return the view to the user.
            return View(companyModel);
        }

        /// <summary>
        /// Controller HTTP Post action method <c>RegisterCompany</c> returns the RegisterCompany View populated with the
        /// company model.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(CompanyModel companyModel)
        {
            // if server-side validation on the Company Model passes, call the applicaiton logic method responsible of registering a
            // company.
            if (ModelState.IsValid)
            {
                CompanyApplicationLogic companyRegistrationLogicObject = new CompanyApplicationLogic(_userManager);

                var RecruiterIdentityCreationState = companyRegistrationLogicObject.CompanyRegistrationLogic(companyModel);

                // If the Recruiter user's identity did not succeed, add the IdentityResult's errors to the model and set the respective
                // boolean value to false.
                if (!RecruiterIdentityCreationState.Succeeded)
                {
                    foreach (var Error in RecruiterIdentityCreationState.Errors)
                    {
                        ModelState.TryAddModelError(Error.Code, Error.Description);
                    }
                    companyModel.SuccessfulRecruiterIdentityRegistrationResponse = false;
                }

                // Else (hence the IdentityResult indicates a successful recruiter user's identity creation, set the respective boolean
                // value to true
                else
                {
                    companyModel.SuccessfulRecruiterIdentityRegistrationResponse = true;
                }
            }

            // If Company Record creation/insertion & the recruiter's user identity were successful (based on their related boolean values)
            // set the CompanyRegistrationAlertID to 1, which will display a successful response message in the view to the user.
            if ((companyModel.SuccessfulCompanyRegistrationResponse == true) && (companyModel.SuccessfulRecruiterIdentityRegistrationResponse == true))
            {
                companyModel.CompanyRegistrationAlertID = 1;
            }

            // Else (hence any of the boolean values is false, reflecting in a failure), set the CompanyRegistrationAlertID to 2, to display
            // a failure reponse message to the user.
            else
            {
                companyModel.CompanyRegistrationAlertID = 2;
            }

            // Get the lists for the view's drop-down menus
            Town town = new Town();
            companyModel.townList = town.GetTownList();

            CompanySize companySize = new CompanySize();
            companyModel.companySizeList = companySize.GetCompanySizeList();

            Industry industry = new Industry();
            companyModel.industryList = industry.GetIndustryList();

            // Return the view with the model.
            return View(companyModel);
        }
    }
}