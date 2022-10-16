using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;

namespace RecruitmentSystemWebApplication.Controllers
{

    /// <summary>
    /// Class <c>JobVacancyController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required for Job Vacancy functionalities.
    /// </summary>

    public class JobVacancyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public JobVacancyController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Controller HTTP Get action method <c>CreateJobVacancy</c> is the controller action method which returns the CreateJobVacancy View
        /// to the user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CreateJobVacancy()
        {
            
            JobVacancyModel jobVacancyModel = new JobVacancyModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            jobVacancyModel.RecruiterUsername = user.UserName;

            // Populate Drop-Down Menus / Select List for the View
            Town town = new Town();
            jobVacancyModel.townList = town.GetTownList();

            EmploymentBasisType employmentBasisType = new EmploymentBasisType();
            jobVacancyModel.employmentBasisTypeList = employmentBasisType.GetEmploymentBasisList();

            YearsOfExperience yearsOfExperience = new YearsOfExperience();
            jobVacancyModel.requiredYearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

            AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
            jobVacancyModel.requiredAcademicEducationQualificationLevelList = requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

            SalaryRange salaryRange = new SalaryRange();
            jobVacancyModel.offeredSalaryRangeList = salaryRange.GetSalaryRangeList();

            JobSpecialization jobSpecialization = new JobSpecialization();
            jobVacancyModel.jobSpecializationList = jobSpecialization.GetJobSpecializationList();

            // Return View with the model to the user
            return View(jobVacancyModel);
        }

        /// <summary>
        /// Controller HTTP Post action method <c>CreateJobVacancy</c> is the controller action method is responsible of handling the UI
        /// logic to create a Job Vacancy after the user submits the respective form from the View.
        /// This action method uses the Applicaiton Logic Layer, which in turn is responsible of calling the Data Access Layer to create 
        /// the job vacancy in the database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateJobVacancy(JobVacancyModel jobVacancyModel)
        {
            // Get Username from the HTTP Context (Security Properties) and set it as the model's username attribute.
            var user = await _userManager.GetUserAsync(HttpContext.User);
            jobVacancyModel.RecruiterUsername = user.UserName;

            // If server-side validation on the model object passes proceed in calling the application logic layer class and method
            // responsible of creating a Job Vacancy, and return the view to the user.
            if (ModelState.IsValid)
            {
                JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
                jobVacancyModel = jobVacancyApplicationLogicObject.CreateJobVacancyApplicationLogic(jobVacancyModel);

                // Populate the model with the lists to be used in the View's Drop-down menus/select lists.
                Town town = new Town();
                jobVacancyModel.townList = town.GetTownList();

                EmploymentBasisType employmentBasisType = new EmploymentBasisType();
                jobVacancyModel.employmentBasisTypeList = employmentBasisType.GetEmploymentBasisList();

                YearsOfExperience yearsOfExperience = new YearsOfExperience();
                jobVacancyModel.requiredYearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

                AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
                jobVacancyModel.requiredAcademicEducationQualificationLevelList = requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

                SalaryRange salaryRange = new SalaryRange();
                jobVacancyModel.offeredSalaryRangeList = salaryRange.GetSalaryRangeList();

                JobSpecialization jobSpecialization = new JobSpecialization();
                jobVacancyModel.jobSpecializationList = jobSpecialization.GetJobSpecializationList();

                return View(jobVacancyModel);
            }

            // Else (hence server-side validation on the model object did not pass) return the view to the user.
            else
            {
                return View(jobVacancyModel);
            }

        }

        /// <summary>
        /// Controller HTTP Get action method <c>ListJobVacancies</c> is the controller action method is responsible of handling the UI
        /// logic to retrieve a list of Job Vacancies. This controller action method calls different application logic layer methods,
        /// depending on the current user's role.
        /// If the current user's role is Recruiter, then it calls the application logic layer method which retrieves all active (not
        /// deleted/deactivated job vacancies) for his/her company.
        /// If the current user does not have the Recruiter role (hence he/she is a signed in jobseeker or unauthenticated user), a list of
        /// all active job vacancies is retrieved. This is achieved by calling the application logic layer method responsible of retrieving
        /// a list of all active job vacancies.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListJobVacancies()
        {
            // If current user has the Recruiter role assigned, retrieve all job vacancies by company.
            // Since a recruiter is related to a single company and vice-versa, the recruiter username is passed onto the logic layer method,
            // responsible of retrieving all active job vacancies by a company.
            if (HttpContext.User.IsInRole("Recruiter"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string RecruiterUsername = user.UserName;
                
                List<JobVacancyModel> jobVacancyList = new List<JobVacancyModel>();
                JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
                
                jobVacancyList = jobVacancyApplicationLogicObject.GetAllJobVacanciesApplicationLogicByCompany(RecruiterUsername);
                return View(jobVacancyList);
            }

            // Else, (hence user is either not signed in or signed in as a jobseeker, return view with all active job vacancies
            else
            {
                List<JobVacancyModel> jobVacancyList = new List<JobVacancyModel>();
                JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
                jobVacancyList = jobVacancyApplicationLogicObject.GetAllJobVacanciesApplicationLogic(jobVacancyList);
                return View(jobVacancyList);
            }
        }

        /// <summary>
        /// Controller <c>ManageJobVacancy</c> handles the UI logic of the Manage Job Vacancy functionality. It retrieves the Job Vacancy
        /// details of the submitted Job Vacancy ID from a view, and returns the ManageJobVacancy view populated with the Job Vacancy details
        /// and several options (including Delete Job Vacancy and View Shortlisted Job Applications) for it.
        /// </summary>
        public async Task<IActionResult> ManageJobVacancy(int JobVacancyID)
        {
            int jobVacancyID = JobVacancyID;
            Console.Write(jobVacancyID.ToString());
            JobVacancyModel jobVacancyModel = new JobVacancyModel();

            JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
            jobVacancyModel = jobVacancyApplicationLogicObject.GetJobVacancyByJobVacancyIDApplicationLogic(JobVacancyID);

            // Populate Job vacancy model with lists to be used with drop down menus in the view. This is done if the recruiter user
            // opts to update the job vacancy (NOTE: Update a Job Vacancy functionality not implemented in its entirety).
            Town town = new Town();
            jobVacancyModel.townList = town.GetTownList();

            EmploymentBasisType employmentBasisType = new EmploymentBasisType();
            jobVacancyModel.employmentBasisTypeList = employmentBasisType.GetEmploymentBasisList();

            YearsOfExperience yearsOfExperience = new YearsOfExperience();
            jobVacancyModel.requiredYearsOfWorkingExperienceList = yearsOfExperience.GetYearsOfExperienceList();

            AcademicEducationQualificationLevel requiredAcademicEducationQualificationLevel = new AcademicEducationQualificationLevel();
            jobVacancyModel.requiredAcademicEducationQualificationLevelList =
                requiredAcademicEducationQualificationLevel.GetAcademicEducationQualificationLevelList();

            SalaryRange salaryRange = new SalaryRange();
            jobVacancyModel.offeredSalaryRangeList = salaryRange.GetSalaryRangeList();

            JobSpecialization jobSpecialization = new JobSpecialization();
            jobVacancyModel.jobSpecializationList = jobSpecialization.GetJobSpecializationList();
            
            // Return View with the job vacancy model object to the user.
            return View(jobVacancyModel);
        }

        /// <summary>
        /// Controller <c>ViewJobVacancy</c> handles the UI logic of the View a Job Vacancy functionality. It retrieves the Job Vacancy
        /// details of the submitted Job Vacancy ID from a view, and returns to the view.
        /// </summary>
        public async Task<IActionResult> ViewJobVacancy(int JobVacancyID)
        {
            JobVacancyModel jobVacancyModelObject = new JobVacancyModel();

            JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
            jobVacancyModelObject = jobVacancyApplicationLogicObject.GetJobVacancyByJobVacancyIDApplicationLogic(JobVacancyID);

            return View(jobVacancyModelObject);
        }

        /// <summary>
        /// Controller <c>DeleteJobVacancy</c> handles the UI logic of the Delete a Job Vacancy functionality. It passes the JobVacancyID
        /// posted from the calling view to the application logic layer method handling a job vacancy deletion.
        /// </summary>
        public async Task<IActionResult> DeleteJobVacancy(int JobVacancyID)
        {
            JobVacancyApplicationLogic jobVacancyApplicationLogicObject = new JobVacancyApplicationLogic();
            JobVacancyModel jobVacancyModelObject = new JobVacancyModel();
            jobVacancyModelObject = jobVacancyApplicationLogicObject.DeleteJobVacancyApplicationLogic(JobVacancyID);

            return View("ManageJobVacancy", jobVacancyModelObject);
        }
    }
}
