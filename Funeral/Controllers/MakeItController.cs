using Funeral.App;
using Funeral.App.Data;
using Funeral.App.Enums;
using Funeral.App.Services;
using Funeral.App.TempData;
using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    public class MakeItController : Controller
    {
        private static Dictionary<string, TempData> tempData = new Dictionary<string, TempData>();

        private readonly IFramesService framesService;
        private readonly ICrossesService crossesService;
        private readonly ITextsService textsService;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment environment;

        public MakeItController(IFramesService framesService, ICrossesService crossesService, ITextsService textsService, IFileService fileService, IWebHostEnvironment environment)
        {
            this.framesService = framesService;
            this.crossesService = crossesService;
            this.textsService = textsService;
            this.fileService = fileService;
            this.environment = environment;
        }

        public IActionResult MakeIt()
        {
            var userName = User.Identity.Name;

            SetInitialValuesToTempData(userName);
            var viewModel = AddValuesToModel(userName);
            return View(viewModel);
        }      

        public IActionResult CreatePdf()
        {

            HtmlToPdf converter = new HtmlToPdf();

            //string urlAddress = "https://www.abv.bg";

            //string data = string.Empty;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    Stream receiveStream = response.GetResponseStream();
            //    StreamReader readStream = null;

            //    if (string.IsNullOrWhiteSpace(response.CharacterSet))
            //        readStream = new StreamReader(receiveStream);
            //    else
            //        readStream = new StreamReader
            //            (receiveStream, Encoding.GetEncoding(response.CharacterSet));

            //    data = readStream.ReadToEnd();

            //    response.Close();
            //    readStream.Close();
            //}

            //converter.Options.Authentication.Username = "stef4otm@gmail.com";
            //converter.Options.Authentication.Password = "Stefi_123";

            PdfDocument doc = converter.ConvertUrl("https://www.abv.bg");

            //PdfDocument doc = converter.ConvertHtmlString(data);
            doc.Save("/Sample.pdf");
            doc.Close();

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeToCrosses()
        {
            tempData[User.Identity.Name].Elements = Elements.Crosses;

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeToFrames()
        {
            tempData[User.Identity.Name].Elements = Elements.Frames;

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeToTexts()
        {
            tempData[User.Identity.Name].Elements = Elements.Texts;

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeFrame(string frameId)
        {
            var framePath = framesService.GetFramePathById(frameId);

            tempData[User.Identity.Name].CurrentFrame = framePath;

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeCross(string crossId)
        {
            var crossPath = crossesService.GetCrossPathById(crossId);

            tempData[User.Identity.Name].CurrentCross = crossPath;

            return RedirectToAction("MakeIt");
        }

        public IActionResult ChangeText(string textId)
        {
            var text = textsService.GetTextById(textId);
            tempData[User.Identity.Name].CurrentText = text;

            return RedirectToAction("MakeIt");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imgFile)
        {
            var path = $"{environment.WebRootPath}/Pictures/UserImages";

            var dir = Directory.CreateDirectory($"{path}/{User.Identity.Name}/");

            var filePath = $"/Pictures/UserImages/{User.Identity.Name}/" + imgFile.FileName;
            var targetDirectory = $"Pictures/UserImages/{User.Identity.Name}";

            await fileService.UploadFile(imgFile, targetDirectory);

            tempData[User.Identity.Name].Picture = filePath;

            await fileService.SaveElementToDb("Picture", filePath);

            return RedirectToAction("MakeIt");
        }

        public IActionResult SaveToDb(SaveToDbInputModel input)
        {
            return Redirect("MakeIt");
        }

        public IActionResult SaveCurrentWork(CurrentWorkInputModel input)
        {
            tempData[User.Identity.Name].CrossText = input.CrossText;
            tempData[User.Identity.Name].AfterCrossText = input.AfterCrossText;
            tempData[User.Identity.Name].FullName = input.FullName;
            tempData[User.Identity.Name].Year = input.Year;
            tempData[User.Identity.Name].CurrentText = input.MainText;
            tempData[User.Identity.Name].Panahida = input.Panahida;
            tempData[User.Identity.Name].From = input.From;

            return RedirectToAction("MakeIt");
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
