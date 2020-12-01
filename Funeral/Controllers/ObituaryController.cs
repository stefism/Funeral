using Funeral.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Funeral.Web.Controllers
{
    public class ObituaryController : Controller
    {
        private readonly IObituaryService obituaryService;

        public ObituaryController(IObituaryService obituaryService)
        {
            this.obituaryService = obituaryService;
        }

        public IActionResult Current(string id)
        {
            var viewModel = obituaryService.GetCurrent(id);

            return View(viewModel);
        }

        public IActionResult AllUserObituary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var model = obituaryService.AllUserObituarys(userId);
            
            return View(model);
        }
    }
}
