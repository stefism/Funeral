using Funeral.App.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IFileService
    {
        public Task UploadFile(IFormFile imgFile, string targetDirectory);

        public void SaveFramePathToDb(string path);

        void SaveCrossPathToDb(string path);

        void DeleteFrameFile(string frameId);
    }
}
