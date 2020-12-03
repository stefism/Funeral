using Funeral.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    public class ObituaryController : Controller
    {
        private readonly IObituaryService obituaryService;

        public ObituaryController(IObituaryService obituaryService)
        {
            this.obituaryService = obituaryService;
        }

        public async Task<IActionResult> Current(string id)
        {
            var viewModel = await obituaryService.GetCurrentAsync(id);

            return View(viewModel);
        }

        public async Task<IActionResult> AllUserObituary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var model = await obituaryService.AllUserObituarysAsync(userId);
            
            return View(model);
        }
    }
}
