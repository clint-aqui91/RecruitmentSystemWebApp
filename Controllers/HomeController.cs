using Microsoft.AspNetCore.Mvc;
using RecruitmentSystemWebApplication.Models;
using System.Diagnostics;
using RecruitmentSystemWebApplication.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Identity;

namespace RecruitmentSystemWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}