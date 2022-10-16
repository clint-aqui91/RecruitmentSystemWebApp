using Microsoft.AspNetCore.Identity;
using RecruitmentSystemWebApplication.DataAccessLayer;
using RecruitmentSystemWebApplication.Models;

namespace RecruitmentSystemWebApplication.ApplicationLogicLayer
{
    /// <summary>
    /// Class <c>JobseekerApplicationLogic</c> holds the application logic layer for a jobseeker model.
    /// In the majority of this class's usage, it gets called by the MVC components (primarly the controller which passes a model to it),
    /// and calls the required classes found in the Data Access layer to access data found in the database.
    /// </summary>
    public class JobseekerApplicationLogic
    {
        UserManager<IdentityUser> _userManager;

        public JobseekerApplicationLogic(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Method <c>JobseekerRegistrationLogic</c> contains the application logic required to register a jobseeker.
        /// Company Registration logic is made up of two  parts, first the jobseeker's user (with Jobseeker role) is created in the
        /// Identity Databaase, then a jobseeker record is inserted in the Application Database. An appropriate feedback message is 
        /// also returned to the user. This method is very similar to the CompanyRegistrationLogic and contains more comments. Refer to
        /// it to further understand this method.
        /// </summary>
        public IdentityResult JobseekerRegistrationLogic(JobseekerModel jobseekerModel)
        {
            string UserRole = "Jobseeker";

            UserIdentityApplicaitonLogic userIdentityApplicaitonLogicObject = new UserIdentityApplicaitonLogic(_userManager);
            var JobseekerIdentityCreationResult = new IdentityResult();


            JobseekerIdentityCreationResult = userIdentityApplicaitonLogicObject.CreateUserIdentity(jobseekerModel.JobseekerEmailAddress,
                                                                                                    jobseekerModel.JobseekerEmailAddress,
                                                                                                    jobseekerModel.JobseekerSignInPassword,
                                                                                                    UserRole).Result;

            // If the jobseeker user identity creation failed, return the Identity Result to the calling method or controller action.
            if (!JobseekerIdentityCreationResult.Succeeded)
            {
                return JobseekerIdentityCreationResult;
            }

            // Else (hence the jobseeker user identity creation succeeeded, proceed in creating the jobseeker record in the Application
            // Database.
            else
            {
                JobseekerDataAccess companyDataAccessObject = new JobseekerDataAccess();
                jobseekerModel = companyDataAccessObject.RegisterJobseeker(jobseekerModel);

                return JobseekerIdentityCreationResult;
            }
        }
    }
}

