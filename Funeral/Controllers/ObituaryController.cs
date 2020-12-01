using Funeral.App.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
