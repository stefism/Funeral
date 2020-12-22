using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IFileService
    {
        public Task UploadFile(IFormFile imgFile, string targetDirectory);

        public void DeleteFile(string frameId);

        public Task DeleteUserPictureFileAsync(string pictureId);

        Task RemoveFrameFromDbAsync(string frameId);

        Task RemoveCrossFromDbAsync(string crossId);

        public Task SaveElementToDbAsync(string elementType, string path, string userId = null);
    }
}
