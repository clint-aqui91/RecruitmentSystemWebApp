using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.ApplicationLogicLayer;

namespace RecruitmentSystemWebApplication.Controllers
{

    /// <summary>
    /// Class <c>FileDownloaderController</c> is the controller class containing the controller action methods which handle the User
    /// Interface logic required when a file is to be sent to the client web browser for download. Images displayed in a view do not use
    /// this controller class, nor its action methods.
    /// </summary>
    public class FileDownloaderController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public FileDownloaderController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Controller Action Method <c>DownloadCVFile</c> calls the Application Logic responsible of retrieving the file bytes array
        /// of a CV File by the JobApplicationID and returns the file to the client web browser for download.
        /// </summary>
        [HttpPost]
        public FileResult DownloadCVFile(int JobApplicationID)
        {
            JobApplicationApplicationLogic jobApplicationApplicationObject = new JobApplicationApplicationLogic();
            Byte[] CVFileBytes = jobApplicationApplicationObject.GetCVFileBytesArrayByJobApplicationID(JobApplicationID);

            // Return a PDF File using the retrieved CVFileBytes & Multipurpose Internet Mail Extensions (MIME) PDF Type to the client
            // web browser for download.
            // Reference for MIME types: https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
            return File(CVFileBytes, "application/pdf");
        }
    }
}
