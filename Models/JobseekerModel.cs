using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RecruitmentSystemWebApplication.Models
{
    /// <summary>
    /// Class <c>JobseekerModel</c> contains the model for a jobseeker.
    /// </summary>
    public class JobseekerModel
    {
        [DisplayName("Jobseeker Name")]
        [Required(ErrorMessage = "Jobseeker Name is required.")]
        [MinLength(3, ErrorMessage = "Jobseeker Name must be at least 3 characters long.")]
        [MaxLength(20, ErrorMessage = "Jobseeker Name not be longer than 20 characters long.")]
        public string? JobseekerName { get; set; }


        [DisplayName("Jobseeker Surname")]
        [Required(ErrorMessage = "Jobseeker surame is required.")]
        [MinLength(3, ErrorMessage = "Jobseeker Surname must be at least 3 characters long.")]
        [MaxLength(30, ErrorMessage = "Jobseeker Surname not be longer than 30 characters long.")]
        public string? JobseekerSurname { get; set; }

        [DisplayName("Jobseeker Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [Required(ErrorMessage = "Jobseeker Email Address is required.")]
        public string? JobseekerEmailAddress { get; set; }

        [DisplayName("Jobseeker Sign-In Password")]
        [Required(ErrorMessage = "Jobseeker Sign-In Password is required.")]
        [DataType(DataType.Password)]
        public string? JobseekerSignInPassword { get; set; }

        [DisplayName("Confirm Password")]
        [Compare("JobseekerSignInPassword", ErrorMessage = "Jobseeker Sign-In Password and confirmation password must match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        // Parameters used to return a feedback messages to a user attempting to register a jobseeker account
        public bool SuccessfuJobseekerIdentityRegistrationResponse { get; set; }
        public bool SuccessfulJobseekerRegistrationResponse { get; set; }

        public int JobseekerRegistrationAlertID { get; set; }
        public string SuccessfulJobseekerRegistrationMessage { get; set; } = "Jobseeker account registered successfully.";
        public string UnsuccessfulJobseekerRegistrationMessage { get; set; } = "Jobseeker account not registered. Something went wrong.";
    }
}
