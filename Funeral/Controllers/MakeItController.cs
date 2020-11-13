﻿using Funeral.App;
using Funeral.App.Enums;
using Funeral.App.Services;
using Funeral.App.TempData;
using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System.Collections.Generic;

namespace Funeral.Web.Controllers
{
    public class MakeItController : Controller
    {
        private static Dictionary<string, TempData> tempData = new Dictionary<string, TempData>();

        private readonly IFramesService framesService;
        private readonly ICrossesService crossesService;
        private readonly ITextsService textsService;

        public MakeItController(IFramesService framesService, ICrossesService crossesService, ITextsService textsService)
        {
            this.framesService = framesService;
            this.crossesService = crossesService;
            this.textsService = textsService;
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

        public IActionResult ChangeToTexts(string textId)
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
