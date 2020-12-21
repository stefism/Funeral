using Funeral.App.Services;
using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IFileService fileService;
        private readonly IFramesService framesService;
        private readonly ICrossesService crossesService;
        private readonly ITextsService textService;

        public AdminController(IFileService fileService, IFramesService framesService, ICrossesService crossesService, ITextsService textService)
        {
            this.fileService = fileService;
            this.framesService = framesService;
            this.crossesService = crossesService;
            this.textService = textService;
        }

        public IActionResult UploadFrame()
        {
            var model = new UploadFrameFilesViewModel();
            model.UploadMessage = "Изберете файл с рамка за качване.";
            model.AllFrames = framesService.ShowAllFrames();

            return View(model);
        }

        public IActionResult UploadCross()
        {
            var model = new UploadCrossesFilesViewModel();
            model.UploadMessage = "Изберете файл с кръст за качване.";
            model.AllCrosses = crossesService.ShowAllCrosses();

            return View(model);
        }

        public IActionResult UploadText()
        {
            var model = textService.ShowAllTexts();

            return View(model);
        }

        public IActionResult DeleteFrame(string frameId)
        {
            string framePath = framesService.GetFramePathById(frameId);
            var model = new AllFramesViewModel
            {
                FilePath = framePath,
                FrameId = frameId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DoDeleteFrame(string frameId)
        {
            string framePath = framesService.GetFramePathById(frameId);

            fileService.DeleteFrameFile(framePath);

            return RedirectToAction("UploadFrame");
        }

        [HttpPost]
        public async Task<IActionResult> UploadText(string funeralText)
        {
            await textService.AddTextToDbAsync(funeralText);

            return RedirectToAction("UploadText");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFrame(IFormFile imgFile)
        {
            var filePath = "/Pictures/Frames/" + imgFile.FileName;
            var targetDirectory = "Pictures/Frames";

            await fileService.UploadFile(imgFile, targetDirectory);

            await fileService.SaveElementToDbAsync("Frame", filePath);

            return RedirectToAction("UploadFrame");
        }

        [HttpPost]
        public async Task<IActionResult> UploadCross(IFormFile imgFile)
        {
            var filePath = "/Pictures/Crosses/" + imgFile.FileName;
            var targetDirectory = "Pictures/Crosses";

            await fileService.UploadFile(imgFile, targetDirectory);

            await fileService.SaveElementToDbAsync("Cross", filePath);

            return RedirectToAction("UploadCross");
        }
    }
}
