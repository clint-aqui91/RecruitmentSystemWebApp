using RecruitmentSystemWebApplication.DataAccessLayer;
using RecruitmentSystemWebApplication.Models;

namespace RecruitmentSystemWebApplication.ApplicationLogicLayer
{
    /// <summary>
    /// Class <c>JobApplicationApplicationLogic</c> holds the application logic layer for a Job Application.
    /// In the majority of this class's usage, it gets called by the MVC components (primarly the controller which passes a model to 
    /// it), and calls the required classes found in the Data Access layer to access data found in the database.
    /// </summary>
    public class JobApplicationApplicationLogic
    {
        /// <summary>
        /// Method <c>GetJobVacancyDetailsByJobVacancyID</c> gets the Job Vacancy Details to be displayed in the View used by a
        /// jobseeker to submit a Job Application.
        /// This method calls the JobVacancyDataAccess class to use its <c>GetJobVacancyByVacancyID</c> method, which in turn return the
        /// job vacancy's details retrieved from the job vacancy record with the matching Vacancy ID.
        /// </summary>
        public JobApplicationModel GetJobVacancyDetailsByJobVacancyID(JobApplicationModel jobApplicationModelObject)
        {
            JobVacancyModel jobVacancyModelObject = new JobVacancyModel();
            JobVacancyDataAccess jobVacancyDataAccessObject = new JobVacancyDataAccess();
            jobVacancyModelObject = jobVacancyDataAccessObject.GetJobVacancyByJobVacancyID(jobApplicationModelObject.JobVacancyID);

            // Populate the Job Application Model with the Company Details found in the Job Vacancy Model Object.
            jobApplicationModelObject.CompanyName = jobVacancyModelObject.CompanyName;
            jobApplicationModelObject.CompanyIndustryName = jobVacancyModelObject.CompanyIndustryName;
            jobApplicationModelObject.CompanyContactEmailAddress = jobVacancyModelObject.CompanyContactEmailAddress;
            jobApplicationModelObject.CompanySizeRange = jobVacancyModelObject.CompanySizeRange;
            jobApplicationModelObject.CompanyLogoFileBytesToBase64 = jobVacancyModelObject.CompanyLogoFileBytesToBase64;
            jobApplicationModelObject.CompanyMainOfficeTownLocation = jobVacancyModelObject.CompanyMainOfficeTownLocation;

            // Populate the Job Application Model Object with the Job Vacancy Details found in the Job Vacancy Model Object.
            jobApplicationModelObject.JobVacancyTitle = jobVacancyModelObject.JobVacancyTitle;
            jobApplicationModelObject.JobVacancyDescription = jobVacancyModelObject.JobVacancyDescription;
            jobApplicationModelObject.JobVacancyRequiredAcademicEducationLevel = jobVacancyModelObject.RequiredAcademicEducationQualificationLevelName;
            jobApplicationModelObject.JobVacancyOfferedSalaryRange = jobVacancyModelObject.OfferedSalaryRangeValue;
            jobApplicationModelObject.JobVacancyLocation = jobVacancyModelObject.JobVacancyLocationName;
            jobApplicationModelObject.JobVacancyRequiredYearsOfExperience = jobVacancyModelObject.RequiredYearsOfWorkingExperienceRangeValue;

            return jobApplicationModelObject;
        }

        /// <summary>
        /// Method <c>CreateJobApplicationApplicationLogic</c> creates a job application record in the application data database.
        /// The method first checks whether another Job Application by the same Jobseeker for the same Job Application and then either
        /// proceeds to create the Job Application record in the database (by using the respective data access layer class/es) or
        /// returns a feedback message to the user stating that another Job Application by the same jobseeker for the same job vacancy
        /// exists.
        /// </summary>
        public JobApplicationModel CreateJobApplicationApplicationLogic(JobApplicationModel jobApplicationModelObject)
        {
            JobApplicationDataAccess jobApplicationDataAccessObject = new JobApplicationDataAccess();
            jobApplicationModelObject = jobApplicationDataAccessObject.CheckJobApplicationExistence(jobApplicationModelObject);

            // If Job Application for the same job vacancy by the same jobseeker exists return the job applicaiton model object
            // back to the calling method or controller action.
            if (jobApplicationModelObject.JobApplicationAlreadyExistsState == true || jobApplicationModelObject.JobApplicationCreationAlertID == 2)
            {
                return jobApplicationModelObject;
            }

            // Else (hence Job application for the same job vacancy by the same jobseeker does not exist, proceed in creating the job
            // application record in the database.
            else
            {
                jobApplicationModelObject = jobApplicationDataAccessObject.InsertJobApplicationRecordInDatabase(jobApplicationModelObject);
                return jobApplicationModelObject;
            }
        }

        /// <summary>
        /// Method <c>GetShortListedJobApplicationByJobVacancyID</c> gets a list of shortlisted job applications (of type JobApplication
        /// model) for a Job Vacancy (by JobVacancyID).
        /// This method calls the JobApplicationDataAccess class found in the Data Access layer, and uses the
        /// GetShortListedJobApplicationByJobVacancyID method to retrive the short listed job applications for the vacancy with the 
        /// matching job vacancy ID.
        /// </summary>
        public List<JobApplicationModel> GetShortListedJobApplicationByJobVacancyID(int JobVacancyID)
        {
            List<JobApplicationModel> shortListedJobApplicationsList = new List<JobApplicationModel>();

            JobApplicationDataAccess jobApplicationDataAccessObject = new JobApplicationDataAccess();
            shortListedJobApplicationsList = jobApplicationDataAccessObject.GetShortListedJobApplicationByJobVacancyID(JobVacancyID);
            return shortListedJobApplicationsList;

        }

        /// <summary>
        /// Method <c>GetCVFileBytesArrayByJobApplicationID</c> uses the <c>JobApplicationDataAccess</c> found in the data access layer 
        /// class to get the file bytes array of a CV File Blob found in the database. The JobApplicaitonID is also passed to the 
        /// access layer.
        /// This method calls the <c>JobApplicationDataAccess</c> class found in the Data Access layer, and uses the
        /// GetShortListedJobApplicationByJobVacancyID method to retrive the short listed job applications for the vacancy with the 
        /// matching job vacancy ID.
        /// </summary>
        public Byte[] GetCVFileBytesArrayByJobApplicationID(int JobApplicationID)
        {
            JobApplicationDataAccess jobApplicationDataAccessObject = new JobApplicationDataAccess();
            return jobApplicationDataAccessObject.GetCVFileBytesByJobApplicationID(JobApplicationID);
        }
    }
}
