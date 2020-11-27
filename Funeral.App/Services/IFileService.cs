using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IFileService
    {
        public Task UploadFile(IFormFile imgFile, string targetDirectory);

        public void SaveFramePathToDb(string path);

        public void SaveCrossPathToDb(string path);

        public void DeleteFrameFile(string frameId);

        public Task SaveElementToDb(string elementType, string path);
    }
}
