using Funeral.App.Services;
using Funeral.App.TempData;
using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Funeral.Web.Controllers
{
    public class MakeItController : Controller
    {
        private static Dictionary<string, TempData> tempData = new Dictionary<string, TempData>();

        private readonly IFramesService framesService;

        public MakeItController(IFramesService framesService) //
        {
            this.framesService = framesService;
        }

        public IActionResult MakeIt(string element)
        {
            var userName = User.Identity.Name;

            if (!tempData.ContainsKey(userName))
            {
                tempData[userName] = new TempData
                {
                    CurrentFrame = "/Pictures/Frames/frame1.gif"
                };
            }

            var viewModel = new MakeItViewModel
            {
                CurrentFrame = tempData[userName].CurrentFrame,
                AllFrames = framesService.ShowAllFrames()
            };


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

        public IActionResult ChangeFrame(string frameId)
        {
            var userName = User.Identity.Name;

            var framePath = framesService.GetFramePathById(frameId);

            tempData[userName].CurrentFrame = framePath;

            //var viewModel = new MakeItViewModel
            //{
            //    CurrentFrame = framePath,
            //    AllFrames = framesService.ShowAllFrames()
            //};

            return RedirectToAction("MakeIt");
        }
    }
}
