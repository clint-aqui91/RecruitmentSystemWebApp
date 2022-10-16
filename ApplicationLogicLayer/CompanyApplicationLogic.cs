using Microsoft.AspNetCore.Identity;
using RecruitmentSystemWebApplication.Models;
using RecruitmentSystemWebApplication.DataAccessLayer;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Web;



namespace RecruitmentSystemWebApplication.ApplicationLogicLayer
{

    /// <summary>
    /// Class <c>CompanyApplicationLogic</c> holds the application logic layer for a company model.
    /// In the majority of this class's usage, it gets called by the MVC components (primarly the controller which passes a model to it),
    /// and calls the required classes found in the Data Access layer to access data found in the database.
    /// </summary>
    public class CompanyApplicationLogic
    {
        // Class which provides the APIs to manage a user found in the Identity store.
        // Reference: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1?view=aspnetcore-6.0
        UserManager<IdentityUser> _userManager;

        // Constructor of class
        public CompanyApplicationLogic(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Method <c>CompanyRegistrationLogic</c> contains the application logic required to register a company.
        /// Company Registration logic is made up of two  parts, first the recruiter's user (with Recruiter role) is created in the
        /// Identity Databaase, then a company record is inserted in the Application Database. An appropriate message is also returned
        /// to the user.
        /// </summary>
        public IdentityResult CompanyRegistrationLogic(CompanyModel companyModel)
        {
            UserIdentityApplicaitonLogic userIdentityApplicaitonLogicObject = new UserIdentityApplicaitonLogic(_userManager);

            // Variable which holds the Identity Result returned from the Create User Identity method.
            var RecruiterUserCreationResult = new IdentityResult();

            // The CreateUserIdentity method found in the UserIdentityApplicaitionLogic class is called to create the recruiter's user
            // and assign the Recruiter role to it. The required parameters are passed onto the CreateUserIdentity method.
            RecruiterUserCreationResult = userIdentityApplicaitonLogicObject.CreateUserIdentity(companyModel.RecruiterSignInEmailAddress,
                                                                                                companyModel.RecruiterSignInEmailAddress,
                                                                                                companyModel.RecruiterSignInPassword,
                                                                                                "Recruiter").Result;

            // If the recruiter's user creation failed, return the result to the controller.
            if (!RecruiterUserCreationResult.Succeeded)
            {
                return RecruiterUserCreationResult;
            }

            // Else (hence the recruiter's user creation was successful), proceed in creating a company record in the application
            // database & return the recruiter's user creation result to the controller.
            else
            {
                CompanyDataAccess companyDataAccessObject = new CompanyDataAccess();
                companyModel = companyDataAccessObject.RegisterCompany(companyModel);

                return RecruiterUserCreationResult;
            }
        }

        /// <summary>
        /// Method <c>UpdateCompanyLogic</c> contains the application logic required to update a company record inside the database.
        /// This method is called by the Company Account Controller, which in turn uses the UpdateCompany method found in the
        /// <c>CompanyDataAccess</c> class and returns the same model object containing the update result to display the appropriate
        /// feedback message to the user in the view.
        /// </summary>
        public CompanyModel UpdateCompanyLogic(CompanyModel companyModel)
        {
            CompanyDataAccess companyDataAccessObject = new CompanyDataAccess();
            companyModel = companyDataAccessObject.UpdateCompany(companyModel);
            return companyModel;
        }

        /// <summary>
        /// Method <c>GetCompanyDetailsByRecruiterUsername</c> contains the application logic required to retrieve the company details
        /// from update a company record inside the database by the recruiter's username.
        /// </summary>
        public CompanyModel GetCompanyDetailsByRecruiterUsername(CompanyModel companyModel)
        {
            CompanyDataAccess companyDataAccessObject = new CompanyDataAccess();
            companyModel = companyDataAccessObject.GetCompanyDetailsByRecruiterUsername(companyModel);
            return (companyModel);
        }
    }
}
