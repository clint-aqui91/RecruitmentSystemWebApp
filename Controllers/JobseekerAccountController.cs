using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;
using RecruitmentSystemWebApplication.Models;

namespace RecruitmentSystemWebApplication.Controllers
{
    /// <summary>
    /// Class <c>JobseekerAccountController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required for for the Jobseeker Account Management functionality.
    /// </summary>
    public class JobseekerAccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;


        public JobseekerAccountController(UserManager<IdentityUser> userManager)//, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
        }

        /// <summary>
        /// Controller Action Method <c>JobseekerAccountController</c> returns the RegisterJobseeker View to the user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> RegisterJobseeker()
        {
            JobseekerModel jobseekerModel = new JobseekerModel();

            return View(jobseekerModel);
        }

        /// <summary>
        /// Controller Action Method <c>JobseekerAccountController</c> handles the UI logic after the user submits the form in the RegisterJobseeker View.
        /// It uses the Application Logic Layer to register/create the Jobseeker's User Identity in the Identity Database and Jobseeker details record in the
        /// Application Database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterJobseeker(JobseekerModel jobseekerModel)
        {
            JobseekerApplicationLogic jobseekerRegistrationLogicObject = new JobseekerApplicationLogic(_userManager);

            var JobseekerIdentityCreationState = jobseekerRegistrationLogicObject.JobseekerRegistrationLogic(jobseekerModel);

            // If the Jobseeker's User Identity Result indicates failure, populate the ModelState with errors from the IdentityResult (these are displayed to
            // the user in the view and set the respective boolean value to false.
            if (!JobseekerIdentityCreationState.Succeeded)
            {
                foreach (var Error in JobseekerIdentityCreationState.Errors)
                {
                    ModelState.TryAddModelError(Error.Code, Error.Description);
                    jobseekerModel.SuccessfuJobseekerIdentityRegistrationResponse = false;
                }
            }

            // Jobseeker's User Identity Result indicates a successful user account creation, then set the boolean value to true.
            else
            {
                jobseekerModel.SuccessfuJobseekerIdentityRegistrationResponse = true;
            }

            // If both boolean values are true, set the JobseekerRegistrationAlertID to 1 to display a succesful feedback message to the user.
            if ((jobseekerModel.SuccessfuJobseekerIdentityRegistrationResponse == true) && (jobseekerModel.SuccessfulJobseekerRegistrationResponse == true))
            {
                jobseekerModel.JobseekerRegistrationAlertID = 1;
            }

            // Else (hence a boolean value is not true indicating an error or failure) set the JobseekerRegistrationAlert to 2 to display a failure feedback
            // message to the user.
            else
            {
                jobseekerModel.JobseekerRegistrationAlertID = 2;
            }

            return View(jobseekerModel);

        }

        /// <summary>
        /// Controller Action Method <c>JobseekerAccountMainMenu</c> handles the UI logic to return the Jobseeker Account Main Menu to the user.
        /// </summary>
        public async Task<IActionResult> JobseekerAccountMainMenu()
        {
            return View();
        }
    }
}
