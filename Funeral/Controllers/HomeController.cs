using Funeral.App.GlobalConstants;
using Funeral.App.SendGrid;
using Funeral.App.Services;
using Funeral.App.ViewModels;
using Funeral.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IGmailService gmailService;
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(
            ILogger<HomeController> logger, 
            RoleManager<IdentityRole> roleManager, 
            ISendGridEmailSender emailSender,
            IGmailService gmailService,
            IUserService userService,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.emailSender = emailSender;
            this.gmailService = gmailService;
            this.userService = userService;
            this.userManager = userManager;
            ;
        }

        public IActionResult Index()
        {
            Partial();
            return View();
        }

        public IActionResult Partial()
        {
            var model = userService.ShowUsersAndPicturesCount();

            return PartialView("_CountsPartial", model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));

            return RedirectToAction("Privacy");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SendToEmail()
        {
            await emailSender.SendEmailAsync("bgfuneral@gmail.com", "Сайта за некролози", "stef4otm@gmail.com", "Test mail", "This is a test mail.");

            return RedirectToAction(nameof(Privacy));
        }

        [Authorize]
        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendFromGmail(EmailInputModel input)
        {
            if (!ModelState.IsValid)
            {               
                return RedirectToAction(nameof(ContactForm));
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userEmail = await this.userManager.GetEmailAsync(user);

            gmailService.SendMailFromGmail(input, userEmail);

            ViewData["ErrorMessage"] = OtherConstants.EmailSendSuccessfully;
            TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

            return RedirectToAction(nameof(SuccessSendedMail));
        }

        public IActionResult SuccessSendedMail()
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
