using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHost;        
        private readonly IEFRepository<Picture> picturesRepository;
        private readonly IEFRepository<Cross> crossesRepository;
        private readonly IEFRepository<Frame> framesRepository;

        public FileService(
            IWebHostEnvironment webHost,
            IEFRepository<Picture> picturesRepository,
            IEFRepository<Cross> crossesRepository,
            IEFRepository<Frame> framesRepository)
        {
            this.webHost = webHost;
            this.picturesRepository = picturesRepository;
            this.crossesRepository = crossesRepository;
            this.framesRepository = framesRepository;
        }

        public void DeleteFile(string filePath)
        {

            string path = "wwwroot" + filePath;

            if (path != null)
            {
                File.Delete(path);
            }
            //TODO: Implement if file not exist.
        }      

        public async Task DeleteUserPictureFileAsync(string pictureId)
        {
            var userPicture = picturesRepository.All()
                .Where(p => p.Id == pictureId).FirstOrDefault();

            var userPicturePath = userPicture.FilePath;
            string filePath = "wwwroot" + userPicturePath;

            if (filePath != null)
            {
                File.Delete(filePath);
            }

            picturesRepository.Delete(userPicture);
            await picturesRepository.SaveChangesAsync();           
        }

        public async Task SaveElementToDbAsync(string elementType, string input, string userId = null)
        {
            if (elementType == "Cross")
            {
                var cross = new Cross
                {
                    FilePath = input,
                };

                await crossesRepository.AddAsync(cross);
                await crossesRepository.SaveChangesAsync();                
            }

            else if (elementType == "Frame")
            {
                var frame = new Frame
                {
                    FilePath = input,
                };

                await framesRepository.AddAsync(frame);
                await framesRepository.SaveChangesAsync();                
            }

            else if (elementType == "Picture")
            {
                var picture = new Picture
                {
                    FilePath = input,
                    UserId = userId,
                };

                await picturesRepository.AddAsync(picture);
                await picturesRepository.SaveChangesAsync();               
            }
        }

        public async Task UploadFile(IFormFile imgFile, string targetDirectory)
        {
            string imagePath = Path.Combine(webHost.WebRootPath, targetDirectory, imgFile.FileName);
            // -> "Pictures/Frames

            using (var uploadImage = new FileStream(imagePath, FileMode.Create))
            {
                await imgFile.CopyToAsync(uploadImage);
                //TODO: Implement if file exist.
            }
        }
    }
}
