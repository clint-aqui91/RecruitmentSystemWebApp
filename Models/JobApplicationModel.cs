using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystemWebApplication.Models
{
    /// <summary>
    /// Class <c>CompanyModel</c> contains the model for a job application.
    /// </summary>
    public class JobApplicationModel
    {
        // Job Application and Job Vacancy IDs, these match the primary keys of the respective database records
        public int JobApplicationID { get; set; }
        public int JobVacancyID { get; set; }

        // Parameters to hold the PossessedYearsOfExerpienceID, PossessedAcademicEducationQualificationLevelID,and PreferedSalaryRangeID chosen by the user
        // from their respective drop-down menus.
        [DisplayName("Required Years of Working Experience Range")]
        [Range(1, 100, ErrorMessage = "Select the Required Years of Working Experience Range")]
        public int PossessedYearsOfWorkingExperienceRangeID { get; set; }
        public string? PossessedYearsOfWorkingExperienceRangeValue { get; set; }

        public List<YearsOfExperience> YearsOfWorkingExperienceList = new List<YearsOfExperience>();

        [DisplayName("Required Academic Education Qualication Level")]
        [Range(1, 100, ErrorMessage = "Select the Required Academic Education Qualification Level")]
        public int PossessedAcademicEducationQualificationLevelID { get; set; }
        public string? PossessedAcademicEducationQualificationLevelName { get; set; }

        public List<AcademicEducationQualificationLevel> AcademicEducationQualificationLevelList = new List<AcademicEducationQualificationLevel>();

        [DisplayName("Preferred Salary Range")]
        [Range(1, 100, ErrorMessage = "Select the Offered Salary Range")]
        public int PreferredSalaryRangeID { get; set; }
        public string? PreferredSalaryRangeValue { get; set; }

        public List<SalaryRange> SalaryRangeList = new List<SalaryRange>();

        [DisplayName("Job Application Covering Letter")]
        [Required(ErrorMessage = "Job Application Covering Letter is required")]
        [MinLength(20, ErrorMessage = "Job Application Covering Letter must be at least 20 characters long.")]
        [MaxLength(400, ErrorMessage = "Job Application Covering Letter must not be longer than 400 characters long.")]
        public string? JobApplicationCoveringLetter { get; set; }

        [DisplayName("CV File")]
        [Required(ErrorMessage = "Please upload file")]
        [DataType(DataType.Upload)]
        public IFormFile? CVFile { get; set; }

        public byte[]? CVFileBytes { get; set; }
        public string? CVFileBytesToBase64 { get; set; }

        // Integer value holding the alert types to display an appropriate message to the user listed in the subsequent string parameters.
        public int JobApplicationCreationAlertID { get; set; } = 0;
        public string SuccessfulJobAppplicationCreationMessage { get; set; } = "Job Application Created Successfully";
        public string UnsuccessfulJobAppplicationCreationMessage { get; set; } = "Job Application not created. Something went wrong.";

        // boolean value representing the existence of a job application for a job vacancy by the same job seeker and a string parameter holding the text displayed as a feedback
        // message to a jobseeker if he/she attempts to create a duplicate job application for the same job vacancy.
        public Boolean JobApplicationAlreadyExistsState { get; set; } = false;
        public string JobApplicationAlreadyExistsMessage { get; set; } = "Job Application for the same Job Vacancy by the same Jobseeker already exists.";

        // Jobseeker details who applied for a job application. These details are displayed together with the details of a job vacancy.
        public int JobseekerID { get; set; }
        public string? JobseekerUsername { get; set; }

        [DisplayName("Jobseeker Name")]
        public string? JobseekerName { get; set; }

        [DisplayName("Jobseeker Surname")]
        public string? JobseekerSurname { get; set; }
        [DisplayName("Jobseeker Email Address")]
        public string? JobseekerContactEmailAddress { get; set; }

        // Company parameters, primarly used to be displayed as company details part of the information displayed together with the job vacancy's details.
        [DisplayName("Company Name")]
        public string? CompanyName { get; set; }

        [DisplayName("Company Headquarters Location")]
        public string? CompanyMainOfficeTownLocation { get; set; }

        [DisplayName("Operating Industry")]
        public string? CompanyIndustryName { get; set; }

        [DisplayName("Company Size")]
        public string? CompanySizeRange { get; set; }

        [DisplayName("Company Contact Email address")]
        public string? CompanyContactEmailAddress { get; set; }

        public byte[]? CompanyLogoFileBytes { get; set; }
        public string? CompanyLogoFileBytesToBase64 { get; set; }

        public int CompanyID { get; set; }

        // Job Vacancy Parameters displayed as job vacancy details
        [DisplayName("Job Vacancy Title")]
        public string? JobVacancyTitle { get; set; }

        [DisplayName("Job Vacancy Offered Salary Range")]
        public string? JobVacancyOfferedSalaryRange { get; set; }

        [DisplayName("Required Years of Experience")]
        public string? JobVacancyRequiredYearsOfExperience { get; set; }

        [DisplayName("Required Academic Education Level")]
        public string? JobVacancyRequiredAcademicEducationLevel { get; set; }

        [DisplayName("Job Vacancy Location")]
        public string? JobVacancyLocation { get; set; }

        [DisplayName("Job Vacancy Description")]
        public string? JobVacancyDescription { get; set; }
    }
}
