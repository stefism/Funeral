using Funeral.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    public class ObituaryController : Controller
    {
        private readonly IObituaryService obituaryService;
        private readonly IUserPictureService userPictureService;

        public ObituaryController(IObituaryService obituaryService, IUserPictureService pictureService)
        {
            this.obituaryService = obituaryService;
            this.userPictureService = pictureService;
        }

        [Authorize]
        public async Task<IActionResult> Current(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = await obituaryService.GetCurrentAsync(id);
            var userPictures = userPictureService.AllUserPictures(userId);
            viewModel.UserPictures = userPictures;

            return View(viewModel);
        }

        public IActionResult UserPictureGalleryPartial()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AllUserObituary()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = await obituaryService.AllUserObituarysAsync(userId);

            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllUserObituaryAdmin(string id)
        {            
            var model = await obituaryService.AllUserObituarysAsync(id);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ChangeUserPicture(string obituaryId, string pictureId)
        {
            await obituaryService.ChangeUserPictureAsync(obituaryId, pictureId);

            return RedirectToAction(nameof(Current), new { id = obituaryId});
        }

        [Authorize]
        public async Task<IActionResult> DeleteObituary(string id)
        {
            await obituaryService.DeleteCurrentUserObituaryAsync(id);

            return RedirectToAction(nameof(AllUserObituary));
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
