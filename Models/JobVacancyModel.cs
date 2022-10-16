//using RecruitmentSystemWebApplication.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;

namespace RecruitmentSystemWebApplication.Models
{

    /// <summary>
    /// Class <c>CompanyModel</c> contains the model for a job vacancy.
    /// </summary>
    public class JobVacancyModel
    {
        public int JobVacancyID { get; set; }

        [DisplayName("Job Vacancy Title")]
        [Required(ErrorMessage = "Job Vacancy Title is required.")]
        [MinLength(5, ErrorMessage = "Job Vacancy name must be at least 5 characters long.")]
        [MaxLength(30, ErrorMessage = "Job Vacancy title must not be longer than 30 characters long.")]
        public string? JobVacancyTitle { get; set; }

        [DisplayName("Job Vacancy Description")]
        [Required(ErrorMessage = "Job Vacancy Description is required")]
        [MinLength(20, ErrorMessage = "Job Vacancy Description must be at least 20 characters long.")]
        [MaxLength(400, ErrorMessage = "Job Vacancy Description must not be longer than 400 characters long.")]
        public string? JobVacancyDescription { get; set; }

        [DisplayName("Job Specialization")]
        //[Range(1, int.MaxValue, ErrorMessage = "Select an appropriate value.")]
        [Range(1, 100, ErrorMessage = "Select an appropriate Job Specialization.")]
        public int JobSpecializationID { get; set; }

        public List<JobSpecialization> jobSpecializationList = new List<JobSpecialization>();
        public string? JobSpecializationName { get; set; }

        [DisplayName("Job Vacancy Location")]
        [Range(1, 100, ErrorMessage = "Select an appropriate town.")]
        public int JobVacancyLocationID { get; set; }

        [DisplayName("Job Vacancy Location")]
        public string? JobVacancyLocationName { get; set; }
        public List<Town> townList = new List<Town>();

        [DisplayName("Employment Basis Type")]
        [Range(1, 100, ErrorMessage = "Select an appropriate Employment Basis Type")]
        public int EmploymentBasisTypeID { get; set; }
        public string? EmploymentBasisTypeName { get; set; }
        public List<EmploymentBasisType> employmentBasisTypeList = new List<EmploymentBasisType>();


        [DisplayName("Required Years of Working Experience Range")]
        [Range(1, 100, ErrorMessage = "Select the Required Years of Working Experience Range")]
        public int RequiredYearsOfWorkingExperienceRangeID { get; set; }

        [DisplayName("Required Years of Working Experience")]
        public string? RequiredYearsOfWorkingExperienceRangeValue { get; set; }
        public List<YearsOfExperience> requiredYearsOfWorkingExperienceList = new List<YearsOfExperience>();

        [DisplayName("Required Academic Education Qualication Level")]
        [Range(1, 100, ErrorMessage = "Select the Required Academic Education Qualification Level")]
        public int RequiredAcademicEducationQualificationLevelID { get; set; }

        [DisplayName("Required Academic Education Level")]
        public string? RequiredAcademicEducationQualificationLevelName { get; set; }
        public List<AcademicEducationQualificationLevel> requiredAcademicEducationQualificationLevelList = new List<AcademicEducationQualificationLevel>();

        [DisplayName("Offered Salary Range")]
        [Range(1, 100, ErrorMessage = "Select the Offered Salary Range")]
        public int OfferedSalaryRangeID { get; set; }

        [DisplayName("Offered Salary Range")]
        public string? OfferedSalaryRangeValue { get; set; }
        public List<SalaryRange> offeredSalaryRangeList = new List<SalaryRange>();

        // Company Attributes
        public int CompanyID { get; set; }
        [DisplayName("Company Name")]
        public string? CompanyName { get; set; }
        [DisplayName("Main Office Location")]
        public string? CompanyMainOfficeTownLocation { get; set; }

        [DisplayName("Operating Industry")]
        public string? CompanyIndustryName { get; set; }

        [DisplayName("Company Size Range")]
        public string? CompanySizeRange { get; set; }

        [DisplayName("Company Contact Email Address")]
        public string? CompanyContactEmailAddress { get; set; }
        public byte[]? CompanyLogoFileBytes { get; set; }
        public string? CompanyLogoFileBytesToBase64 { get; set; }
        public string? RecruiterUsername { get; set; }

        public List<JobVacancyModel>? JobVacancyList;

        public int JobVacancyCreationAlertID { get; set; } = 0;
        public string SuccessfulJobVacancyCreationMessage { get; set; } = "Job Vacancy Created Successfully";
        public string UnsuccessfulJobVacancyCreationMessage { get; set; } = "Job Vacancy not created. Something went wrong.";

        // Integer value holding the alert types to display an appropriate message to the user listed in the subsequent string parameters.
        public int JobVacancyUpdateAlertID { get; set; } = 0;
        public string SuccessfulJobVacancyUpdateMessage { get; set; } = "Job Vacancy Updated Successfully.";
        public string UnsuccessfulJobVacancyUpdateMessage { get; set; } = "Job Vacanncy not updated. Something went wrong.";
        public int JobVacancyDeleteAlertID { get; set; } = 0;
        public string SuccessfulJobVacancyDeleteMessage { get; set; } = "Job Vacancy Deleted Successfully.";
        public string UnsuccessfulJobVacancyDeleteMessage { get; set; } = "Job Vacanncy not Deleted. Something went wrong.";

    }
}
