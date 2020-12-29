using Funeral.App.Enums;
using Funeral.App.GlobalConstants;
using Funeral.App.Services;
using Funeral.App.TempData;
using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    [Authorize]
    public class MakeItController : Controller
    {
        private static Dictionary<string, TempData> tempData = new Dictionary<string, TempData>();

        private readonly IFramesService framesService;
        private readonly ICrossesService crossesService;
        private readonly ITextsService textsService;
        private readonly IObituaryService obituaryService;
        private readonly IFileService fileService;
        private readonly IUserPictureService userPictureService;
        private readonly IWebHostEnvironment environment;

        private string UserId =>
            User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public MakeItController(
            IFramesService framesService,
            ICrossesService crossesService,
            ITextsService textsService,
            IObituaryService obituaryService,
            IFileService fileService,
            IUserPictureService userPictureService,
            IWebHostEnvironment environment)
        {
            this.framesService = framesService;
            this.crossesService = crossesService;
            this.textsService = textsService;
            this.obituaryService = obituaryService;
            this.fileService = fileService;
            this.userPictureService = userPictureService;
            this.environment = environment;
        }

        public IActionResult MakeIt()
        {
            var userName = User.Identity.Name;

            SetInitialValuesToTempData(userName);
            var viewModel = AddValuesToModel(userName);
            return View(viewModel);
        }

        public IActionResult MakeItWithoutPicture()
        {
            var userName = User.Identity.Name;

            SetInitialValuesToTempData(userName);
            var viewModel = AddValuesToModel(userName);
            return View(viewModel);
        }

        public IActionResult ClearUserTempData()
        {
            tempData.Remove(User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeToCrosses(string type)
        {
            tempData[User.Identity.Name].Elements = Elements.Crosses;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        public IActionResult ChangeToFrames(string type)
        {
            tempData[User.Identity.Name].Elements = Elements.Frames;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        public IActionResult ChangeToTexts(string type)
        {
            tempData[User.Identity.Name].Elements = Elements.Texts;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        public IActionResult ChangeFrame(string frameId, string type)
        {
            var framePath = framesService.GetFramePathById(frameId);

            tempData[User.Identity.Name].CurrentFrame = framePath;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        public async Task<IActionResult> ChangeCross(string crossId, string type)
        {
            var crossPath = crossesService.GetCrossPathByIdAsync(crossId);

            tempData[User.Identity.Name].CurrentCross = await crossPath;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        public IActionResult ChangeText(string textId, string type)
        {
            var text = textsService.GetTextById(textId);
            tempData[User.Identity.Name].CurrentText = text;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imgFile)
        {
            if (imgFile == null)
            {
                ViewData["ErrorMessage"] = ErrorConstants.NoSelectedFileError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            string imageExt = Path.GetExtension(imgFile.FileName);

            var picCount = userPictureService.GetUserPictureCount(UserId);

            if (picCount >= OtherConstants.MaxNumberOfUserPicture)
            {
                ViewData["ErrorMessage"] = ErrorConstants.MaxUserPictureError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            if (imageExt != ".jpg" && imageExt != ".png" && imageExt != ".gif")
            {
                ViewData["ErrorMessage"] = ErrorConstants.FileTypeError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            if (imgFile.Length > OtherConstants.PeoplePictureMaxSigeInBytes)
            {
                ViewData["ErrorMessage"] = ErrorConstants.FileMaxSizeError;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            var stream = imgFile.OpenReadStream();
            bool isImage = IsImage(stream);

            if (!isImage)
            {
                ViewData["ErrorMessage"] = ErrorConstants.ImageIsNotImage;
                TempData.Add("ErrorMessage", ViewData["ErrorMessage"]);

                return RedirectToAction("Error", "Errors");
            }

            var path = $"{environment.WebRootPath}/Pictures/UserImages";

            var dir = Directory.CreateDirectory($"{path}/{User.Identity.Name}/");

            var filePath = $"/Pictures/UserImages/{User.Identity.Name}/" + imgFile.FileName;
            var targetDirectory = $"Pictures/UserImages/{User.Identity.Name}";

            await fileService.UploadFile(imgFile, targetDirectory);

            tempData[User.Identity.Name].Picture = filePath;

            await fileService.SaveElementToDbAsync("Picture", filePath, UserId);

            return RedirectToAction("MakeIt");
        }

        public async Task<IActionResult> SaveToDataBaseAsync(SaveToDbInputModel input)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var obituaryId = await obituaryService.SaveToDbAsync(input, userId);

            return Redirect($"/Obituary/Current?id={obituaryId}");
        }

        public IActionResult SaveCurrentWork(CurrentWorkInputModel input, string type)
        {
            tempData[User.Identity.Name].CrossText = input.CrossText;
            tempData[User.Identity.Name].AfterCrossText = input.AfterCrossText;
            tempData[User.Identity.Name].FullName = input.FullName;
            tempData[User.Identity.Name].Year = input.Year;
            tempData[User.Identity.Name].CurrentText = input.MainText;
            tempData[User.Identity.Name].Panahida = input.Panahida;
            tempData[User.Identity.Name].From = input.From;

            if (type == "no")
            {
                return RedirectToAction(nameof(MakeItWithoutPicture));
            }

            return RedirectToAction(nameof(MakeIt));
        }

        private bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }

        private MakeItViewModel AddValuesToModel(string userName)
        {
            return new MakeItViewModel
            {
                CurrentFrame = tempData[userName].CurrentFrame,
                CurrentCross = tempData[userName].CurrentCross,
                CurrentText = tempData[userName].CurrentText,
                CrossText = tempData[userName].CrossText,
                AfterCrossText = tempData[userName].AfterCrossText,
                FullName = tempData[userName].FullName,
                Panahida = tempData[userName].Panahida,
                From = tempData[userName].From,
                Year = tempData[userName].Year,
                Picture = tempData[userName].Picture,
                AllFrames = framesService.ShowAllFrames(),
                AllCrosses = crossesService.ShowAllCrosses(),
                AllTexts = textsService.ShowAllTexts(),
                Elements = tempData[userName].Elements,
            };
        }

        private void SetInitialValuesToTempData(string userName)
        {
            if (!tempData.ContainsKey(userName))
            {
                tempData[userName] = new TempData
                {
                    CurrentFrame = StringConstants.DefaultFramePath,
                    CurrentCross = StringConstants.DefaultCrossPath,
                    CrossText = StringConstants.DefaultCrossText,
                    AfterCrossText = StringConstants.DefaultAfterCrossText,
                    FullName = StringConstants.DefaultFullName,
                    Picture = StringConstants.DefaultPicturePath,
                    Panahida = StringConstants.DefaultPanahidaText,
                    From = StringConstants.DefaultFromText,
                    Year = StringConstants.DefaultYearText,
                    CurrentText = textsService.ReturnFirtsText(),
                    Elements = Elements.Frames,
                };
            }
        }
    }
}
