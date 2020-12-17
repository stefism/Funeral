using Funeral.App.SendGrid;
using Funeral.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Funeral.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ISendGridEmailSender emailSender;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, ISendGridEmailSender emailSender)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
            ;
        }

        public IActionResult Index()
        {       
            return View();
        }

        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));

            return RedirectToAction("Privacy");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> SendToEmail()
        {         
            await emailSender.SendEmailAsync("bgfuneral@gmail.com", "Сайта за некролози", "stef4otm@gmail.com", "Test mail", "This is a test mail.");

            return RedirectToAction(nameof(Privacy));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
