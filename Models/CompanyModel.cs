//using RecruitmentSystemWebApplication.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using RecruitmentSystemWebApplication.DataAccessLayer.DropDownLists;

namespace RecruitmentSystemWebApplication.Models
{

    /// <summary>
    /// Class <c>CompanyModel</c> contains the model for a company.
    /// </summary>
    public class CompanyModel
    {
        // CompanyName parameter holds the Company Name.
        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company Name is required.")]
        [MinLength(5, ErrorMessage = "Company Name must be at least 5 characters long.")]
        [MaxLength(30, ErrorMessage = "Company Name must not be longer than 30 characters long.")]
        public string CompanyName { get; set; }

        // CompanyContactEmailAddress parameter holds the Company Name.
        [DisplayName("Contact Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [Required(ErrorMessage = "Contact Email Address is required.")]
        public string? CompanyContactEmailAddress { get; set; }

        // RecruiterSignInEmailAddress parameter holds the recruiter's username.
        [DisplayName("Sign-In Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [Required(ErrorMessage = "Sign-Email Address is required.")]
        public string? RecruiterSignInEmailAddress { get; set; }

        // RecruiterSignInPassword parameter holds the recruiter's passsword
        [DisplayName("Recruiter Sign-In Password")]
        [Required(ErrorMessage = "Recruiter Sign-In Password is required.")]
        [DataType(DataType.Password)]
        public string? RecruiterSignInPassword { get; set; }

        // ConfirmPassword parameter value must match the Confirm Password
        [DisplayName("Confirm Password")]
        [Compare("RecruiterSignInPassword", ErrorMessage = "Recruiter Sign-In Password and confirmation password must match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        // Parameters to hold the IndustryID, TownID, CompanySizeID chosen by the user from their respective drop-down menus.
        [DisplayName("Industry Name")]
        [Range(1, 100, ErrorMessage = "Select an appropriate Industry Name")]
        public int IndustryID { get; set; }
        
        public List<Industry> industryList = new List<Industry>();
        //public string IndustryName { get; set; }

        [DisplayName("Town Name")]
        [Range(1, 100, ErrorMessage = "Select an appropriate Town.")]
        public int TownID { get; set; }

        public List<Town> townList = new List<Town>();
        //public string TownName { get; set; }
        
        [DisplayName("Company Size")]
        [Range(1, 100, ErrorMessage = "Select an appropriate Company Size")]
        public int CompanySizeID { get; set; }
        
        public List<CompanySize> companySizeList = new List<CompanySize>();
        //public string CompanySizeName { get; set; }

        // Company Logo File uploaded from the View's form by the user.
        [DisplayName("Company Logo File")]
        [Required(ErrorMessage = "Please upload file")]
        [DataType(DataType.Upload)]
        public IFormFile? CompanyLogoFile {  get; set;}

        // Byte Array to hold the bytes of the uploaded Company Logo File
        public byte[]? FileBytes { get; set; }

        // CompanyID which matches the company's Primary Key from the database
        int CompanyID { get; set; }
        
        // boolean values reflecting the registration of the recruiter's identity and company results
        public bool SuccessfulRecruiterIdentityRegistrationResponse { get; set; }
        public bool SuccessfulCompanyRegistrationResponse { get; set; }
        
        // Integer value holding the alert types to display an appropriate message to the user listed in the subsequent string parameters. 
        public int CompanyRegistrationAlertID { get; set; }
        public string SuccessfulCompanyRegistrationMessage { get; set; } = "Company Registered Successfully";
        public string UnsuccessfulCompanyRegistrationMessage { get; set; } = "Company not registered. Something went wrong.";

        public int CompanyUpdateAlertID { get; set; }
        public string SuccessfulCompanyUpdateMessage { get; set; } = "Company updated successfully.";
        public string UnsuccessfulCompanyUpdateMessage { get; set; } = "Company not updated. Something went wrong";
    }
}
