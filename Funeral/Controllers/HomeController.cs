using Funeral.App.VirtualDb;
using Funeral.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Funeral.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VirtualDbContext virtualDb;

        public HomeController(ILogger<HomeController>
            logger, VirtualDbContext virtualDb)
        {
            _logger = logger;
            this.virtualDb = virtualDb;
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
