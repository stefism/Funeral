using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IFileService
    {
        public Task UploadFile(IFormFile imgFile, string targetDirectory);

        public void DeleteFrameFile(string frameId);

        public Task SaveElementToDbAsync(string elementType, string path, string userId = null);
    }
}
