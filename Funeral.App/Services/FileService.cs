using Funeral.App.Data;
using Funeral.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext db;

        public FileService(IWebHostEnvironment webHost, ApplicationDbContext db)
        {
            this.webHost = webHost;
            this.db = db;
        }

        public void DeleteFrameFile(string framePath)
        {

            string filePath = "wwwroot" + framePath;

            if (filePath != null)
            {
                File.Delete(filePath);
            }
        }

        public async Task SaveElementToDbAsync(string elementType, string input, string userId = null)
        {    
            if (elementType == "Cross")
            {
                var cross = new Cross
                {
                    FilePath = input,
                };

                await db.Crosses.AddAsync(cross);
                await db.SaveChangesAsync();
            }

            else if (elementType == "Frame")
            {
                var frame = new Frame
                {
                    FilePath = input,
                };

                await db.Frames.AddAsync(frame);
                await db.SaveChangesAsync();
            }

            else if (elementType == "Picture")
            {
                var picture = new Picture
                {
                    FilePath = input,
                    UserId = userId,
                };

                await db.Pictures.AddAsync(picture);
                await db.SaveChangesAsync();
            }
        }

        public async Task UploadFile(IFormFile imgFile, string targetDirectory)
        {
            string imagePath = Path.Combine(webHost.WebRootPath, targetDirectory, imgFile.FileName);
            // -> "Pictures/Frames

            using (var uploadImage = new FileStream(imagePath, FileMode.Create))
            {
                await imgFile.CopyToAsync(uploadImage);
            }
        }
    }
}
