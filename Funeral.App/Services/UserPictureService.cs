using Funeral.App.ViewModels;
using Funeral.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class UserPictureService : IUserPictureService
    {
        private readonly ApplicationDbContext db;

        public UserPictureService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int GetUserPictureCount(string userId)
        {
            return db.Pictures.Where(p => p.UserId == userId).Count();
        }

        public IEnumerable<UserPictureViewModel> AllUserPictures(string userId)
        {
            return db.Pictures.Where(p => p.UserId == userId)
                .Select(p => new UserPictureViewModel
                {
                    PictureId = p.Id,
                    PictureFilePath = p.FilePath,
                });
        }

        public UserPictureViewModel CurrentUserPicure(string pictureId)
        {
            return db.Pictures.Where(p => p.Id == pictureId)
                .Select(p => new UserPictureViewModel
                {
                    PictureId = p.Id,
                    PictureFilePath = p.FilePath,
                }).FirstOrDefault();
        }

        public async Task RemovePictureIdFromUserObityarysAsync(string pictureId)
        {
            var userObituarys = db.Obituaries.Where(o => o.PictureId == pictureId);

            if (userObituarys != null)
            {
                foreach (var obituary in userObituarys)
                {
                    obituary.PictureId = null;
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
