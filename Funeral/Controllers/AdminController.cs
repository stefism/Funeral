using Funeral.App.GlobalConstants;
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

        public AdminController(
            IFileService fileService, 
            IFramesService framesService, 
            ICrossesService crossesService, 
            ITextsService textService)
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

        [HttpPost]
        public async Task<IActionResult> UploadFrame(IFormFile imgFile)
        {
            if (imgFile == null)
            {
                ViewData["ErrorMessage"] = ErrorConstants.NoSelectedFileError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            var filePath = "/Pictures/Frames/" + imgFile.FileName;
            var targetDirectory = "Pictures/Frames";

            await fileService.UploadFile(imgFile, targetDirectory);

            await fileService.SaveElementToDbAsync("Frame", filePath);

            return RedirectToAction(nameof(UploadFrame));
        }

        public IActionResult UploadCross()
        {
            var model = new UploadCrossesFilesViewModel();
            model.UploadMessage = "Изберете файл с кръст за качване.";
            model.AllCrosses = crossesService.ShowAllCrosses();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCross(IFormFile imgFile)
        {
            if (imgFile == null)
            {
                ViewData["ErrorMessage"] = ErrorConstants.NoSelectedFileError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            var filePath = "/Pictures/Crosses/" + imgFile.FileName;
            var targetDirectory = "Pictures/Crosses";

            await fileService.UploadFile(imgFile, targetDirectory);

            await fileService.SaveElementToDbAsync("Cross", filePath);

            return RedirectToAction(nameof(UploadCross));
        }

        public IActionResult UploadText()
        {
            var model = textService.ShowAllTexts();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadText(string funeralText)
        {
            await textService.AddTextToDbAsync(funeralText);

            return RedirectToAction(nameof(UploadText));
        }

        public IActionResult DeleteTextTemplate(TextTemplateViewModel input)
        {
            var viewModel = new TextTemplateViewModel
            {
                TextId = input.TextId,
                TextTemplate = input.TextTemplate,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DoDeleteTextTemplate(string textTemplateId)
        {
            await textService.DeleteTextTemplateAsync(textTemplateId);

            return RedirectToAction(nameof(UploadText));
        }

        public IActionResult EditTextTemplate(TextTemplateViewModel input)
        {
            var viewModel = new TextTemplateViewModel
            {
                TextId = input.TextId,
                TextTemplate = input.TextTemplate,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DoEditTextTemplate(string textId, string editedText)
        {
            await textService.EditTextTemplateAsync(textId, editedText);

            return RedirectToAction(nameof(UploadText));
        }

        public IActionResult DeleteFrame(string frameId)
        {
            string framePath = framesService.GetFramePathById(frameId);
            var model = new FrameViewModel
            {
                FilePath = framePath,
                FrameId = frameId
            };

            return View(model);
        }

        public async Task<IActionResult> DeleteCross(string crossId)
        {
            string crossPath = await crossesService.GetCrossPathByIdAsync(crossId);
            var model = new CrossViewModel
            {
                FilePath = crossPath,
                CrossId = crossId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DoDeleteFrame(string frameId)
        {
            string framePath = framesService.GetFramePathById(frameId);
            await fileService.RemoveFrameFromDbAsync(frameId);

            fileService.DeleteFile(framePath);

            return RedirectToAction(nameof(UploadFrame));
        }

        [HttpPost]
        public async Task<IActionResult> DoDeleteCross(string crossId)
        {
            string crossPath = await crossesService.GetCrossPathByIdAsync(crossId);
            await fileService.RemoveCrossFromDbAsync(crossId);

            fileService.DeleteFile(crossPath);
            
            return RedirectToAction(nameof(UploadCross));
        }
    }
}
