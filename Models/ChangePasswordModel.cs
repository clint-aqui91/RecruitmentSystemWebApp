using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentSystemWebApplication.Models
{
    /// <summary>
    /// Class <c>ChangePasswordModel</c> contains the model for the change password functionality.
    /// </summary>
    public class ChangePasswordModel
    {
        // Username parameter used to hold the user's username
        [DisplayName("Username")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [Required(ErrorMessage = "Sign-In Email Address is required.")]
        public string? Username { get; set; }

        // Username parameter used to hold the user's current password, to be inputted by the user in the View's form.
        // As a security best practice, the current password should be required when changing a password, to prevent the risk of having a malicious actor with access to an unattended
        // device signed into the application being capable to change the password without knowing the current password.
        [DisplayName("Current Password")]
        [Required(ErrorMessage = "Current Password is required.")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        // New Password parameter.
        [DisplayName("New Password")]
        [Required(ErrorMessage = "New Password is required.")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        // Confirm Password parameter, which must match the NewPassword parameter.
        [DisplayName("Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation password must match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        // Integer value holding the Password Change Result state, to display an appropriate feedback message to the user, following a password change attempt.
        // Boolean type cannot be used, since it holds two states (true or false), and an inappropriate message will be displayed to the user when the view is returned the first time.
        public int PasswordChangeAlertID { get; set; }
        
        // String parameter holding the text used as a feedback message to the user indicating that the password change attempt was successful.
        public string? SuccessfulPasswordChangeMessage { get; set; } = "Password changed successfully.";

        // String parameter holding the text used as a feedback message to the user indicating that the password change attempt was unsuccessful/failed.
        public string? UnsuccessfulPasswordChangeMessage { get; set; } = "Password not changed successfully.";
    }
}
