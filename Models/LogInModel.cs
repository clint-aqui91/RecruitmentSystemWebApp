using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystemWebApplication.Models
{
    /// <summary>
    /// Class <c>LogInModel</c> contains the model for the login/sign-in functionality.
    /// </summary>
    public class LogInModel
    {
        [DisplayName("Sign-In Email Address")]
        [Required(ErrorMessage = "Sign-In Email Address is required")]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }

        [DisplayName("Sign-In Password")]
        [Required(ErrorMessage = "Sign-In Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
