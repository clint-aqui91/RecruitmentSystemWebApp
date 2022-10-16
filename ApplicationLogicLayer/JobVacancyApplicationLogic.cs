using RecruitmentSystemWebApplication.DataAccessLayer;
using RecruitmentSystemWebApplication.Models;

namespace RecruitmentSystemWebApplication.ApplicationLogicLayer
{
    /// <summary>
    /// Class <c>JobVacancyApplicationLogic</c> holds the application logic layer for a job vacancy model.
    /// In the majority of this class's usage, it gets called by the MVC components (primarly the controller which passes a model to it),
    /// and calls the required classes found in the Data Access layer to access data found in the database.
    /// </summary>
    public class JobVacancyApplicationLogic
    {
        /// <summary>
        /// Method <c>CreateJobVacancyApplicationLogic</c> holds the application logic required to create a Job Vacancy in the database.
        /// This method uses the <c>InsertJobVacancyRecordInDatabase</c> found in the JobVacancyDataAccess class located inside the Data Access layer,
        /// which in turn interacts directly with the database, to create the Job Vacancy record through a Stored Procedure, populates the
        /// model with Result IDs to be used to return feedback messages to the user.
        /// </summary>
        public JobVacancyModel CreateJobVacancyApplicationLogic(JobVacancyModel jobVacancyModel)
        {
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();
            jobVacancyModel = jobVacancyDataAccessObject.InsertJobVacancyRecordInDatabase(jobVacancyModel);
            return jobVacancyModel;
        }

        /// <summary>
        /// Method <c>GetAllJobVacanciesApplicationLogic</c> holds the application logic required to retrieve all the job vacancies.
        /// This method uses the <c>GetAllJobVacancies</c> method found in the JobVacancyDataAccess class located inside the Data Access
        /// layer.
        /// </summary>
        public List<JobVacancyModel> GetAllJobVacanciesApplicationLogic(List<JobVacancyModel> jobVacancyList)
        {
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();

            jobVacancyList = jobVacancyDataAccessObject.GetAllJobVacancies(jobVacancyList);
            return jobVacancyList;
        }

        /// <summary>
        /// Method <c>GetAllJobVacanciesApplicationLogicByCompany</c> holds the application logic required to retrieve all the job vacancies
        /// for a company (by the Recruiter's Username). Since a single company is related to a single recruiter, the recruiter's username
        /// is used to identify the company.
        /// </summary>
        public List<JobVacancyModel> GetAllJobVacanciesApplicationLogicByCompany(string recruiterUsername)
        {
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();
            List<JobVacancyModel> jobVacancyList = new List<JobVacancyModel>();

            jobVacancyList = jobVacancyDataAccessObject.GetAllJobVacanciesByCompany(recruiterUsername);
            return jobVacancyList;
        }

        /// <summary>
        /// Method <c>GetJobVacancyByJobVacancyIDApplicationLogic</c> holds the application logic required to retrieve a job vacancy by
        /// its ID from the Data Access layer.
        /// </summary>
        public JobVacancyModel GetJobVacancyByJobVacancyIDApplicationLogic(int jobVacancyID)
        {
            JobVacancyModel jobVacancyModel = new JobVacancyModel();
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();

            jobVacancyModel = jobVacancyDataAccessObject.GetJobVacancyByJobVacancyID(jobVacancyID);
            return jobVacancyModel;
        }

        /// <summary>
        /// Method <c>DeleteJobVacancyApplicationLogic</c> holds the application logic required to delete a job vacancy by using the
        /// Data Access Layer. It passes the JobVacanyID of the JobVacancy to be deleted to the Data Access Layer.
        /// </summary>
        public JobVacancyModel DeleteJobVacancyApplicationLogic(int jobVacancyID)
        {
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();
            JobVacancyModel jobVacancyModelObject = new JobVacancyModel();

            jobVacancyModelObject = jobVacancyDataAccessObject.DeleteJobVacancy(jobVacancyID);
            return jobVacancyModelObject;
        }

    }
}
