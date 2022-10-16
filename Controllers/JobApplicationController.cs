using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;
using RecruitmentSystemWebApplication.Models;

namespace RecruitmentSystemWebApplication.Controllers
{
    /// <summary>
    /// Class <c>JobApplicationController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required for Job Application functionalities.
    /// </summary>
    public class JobApplicationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public JobApplicationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Class <c>CreateJobApplication</c> is the controller action method which gets the details of a job vacancy and returns the
        /// CreateJobApplication View. It uses the JobApplicationApplicationLogic class to retrive the Job Vacancy Details.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CreateJobApplication(int JobVacancyID)
        {
            JobApplicationModel jobApplicationModelObject = new JobApplicationModel();
            jobApplicationModelObject.JobVacancyID = JobVacancyID;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            jobApplicationModelObject.JobseekerUsername = user.UserName;

            // Get Job Vacancy Details using the Application Logic Layer.
            JobApplicationApplicationLogic jobApplicationApplicationLogicObject = new JobApplicationApplicationLogic();
            jobApplicationModelObject = jobApplicationApplicationLogicObject.GetJobVacancyDetailsByJobVacancyID(jobApplicationModelObject);

            // Populate the View's Dropdown Menu/ Select lists
            YearsOfExperience yearsOfExperience = new YearsOfExperience();
            jobApplicationModelObject.YearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

            AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
            jobApplicationModelObject.AcademicEducationQualificationLevelList =
                requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

            SalaryRange salaryRange = new SalaryRange();
            jobApplicationModelObject.SalaryRangeList = salaryRange.GetSalaryRangeList();

            return View(jobApplicationModelObject);
        }

        /// <summary>
        /// Controller Action Post Method <c>CreateJobApplication</c> is the action method which responsible of creating the job application
        /// after the user submits the Create Job Applicaiton form from the respective view.
        /// It uses the JobApplicationApplicationLogic class to create the Job Application.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateJobApplication(JobApplicationModel jobApplicationModelObject)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            jobApplicationModelObject.JobseekerUsername = user.UserName;

            // If server-side validation passes, call the Application Logic Layer to create the Job Application.
            if (ModelState.IsValid)
            {
                // Create Job Application
                JobApplicationApplicationLogic jobApplicationApplicationLogicObject = new JobApplicationApplicationLogic();
                jobApplicationModelObject = jobApplicationApplicationLogicObject.CreateJobApplicationApplicationLogic(jobApplicationModelObject);

                // Populate Job Application Model Object with related Job Vacancy Details
                jobApplicationModelObject = jobApplicationApplicationLogicObject.GetJobVacancyDetailsByJobVacancyID(jobApplicationModelObject);

                // Populate Dropdown lists
                YearsOfExperience yearsOfExperience = new YearsOfExperience();
                jobApplicationModelObject.YearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

                AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
                jobApplicationModelObject.AcademicEducationQualificationLevelList =
                    requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

                SalaryRange salaryRange = new SalaryRange();
                jobApplicationModelObject.SalaryRangeList = salaryRange.GetSalaryRangeList();

                Console.WriteLine("Job Application created");

                // return View with the Job Application Model Object
                return View(jobApplicationModelObject);
            }

            // Else (hence server-side validation on the model object does not pass), get the Job Vacancy Details, get the drop-down/select lists
            // and return the view to the user.
            else
            {
                JobApplicationApplicationLogic jobApplicationApplicationLogicObject = new JobApplicationApplicationLogic();
                jobApplicationModelObject = jobApplicationApplicationLogicObject.GetJobVacancyDetailsByJobVacancyID(jobApplicationModelObject);

                YearsOfExperience yearsOfExperience = new YearsOfExperience();
                jobApplicationModelObject.YearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

                AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
                jobApplicationModelObject.AcademicEducationQualificationLevelList =
                    requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

                SalaryRange salaryRange = new SalaryRange();
                jobApplicationModelObject.SalaryRangeList = salaryRange.GetSalaryRangeList();

                Console.WriteLine("Job Application not created");

                return View(jobApplicationModelObject);
            }
        }

        /// <summary>
        /// Controller Action Method <c>ListShortlistedJobApplicationsByJobVacancyID</c> retrives the shortlisted Job Application for a 
        /// Job Vacancy (by the respective Job Vacancy's ID.
        /// </summary>
        public async Task<IActionResult> ListShortlistedJobApplicationsByJobVacancyID(int JobVacancyID)
        {

            List<JobApplicationModel> shortListedJobApplicationsList = new List<JobApplicationModel>();

            JobApplicationApplicationLogic jobApplicationApplicationLogicObject = new JobApplicationApplicationLogic();
            shortListedJobApplicationsList = jobApplicationApplicationLogicObject.GetShortListedJobApplicationByJobVacancyID(JobVacancyID);

            return View(shortListedJobApplicationsList);
        }
    }
}
