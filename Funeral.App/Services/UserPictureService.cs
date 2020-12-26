using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class UserPictureService : IUserPictureService
    {
        private readonly IEFRepository<Picture> picturesRepository;
        private readonly IEFRepository<Obituary> obituarysRepository;

        public UserPictureService(
            IEFRepository<Picture> picturesRepository,
             IEFRepository<Obituary> obituarysRepository)
        {
            this.picturesRepository = picturesRepository;
            this.obituarysRepository = obituarysRepository;
        }

        public int GetUserPictureCount(string userId)
        {
            return picturesRepository.All()
                .Where(p => p.UserId == userId).Count();
        }

        public IEnumerable<UserPictureViewModel> AllUserPictures(string userId)
        {
            return picturesRepository.All()
                .Where(p => p.UserId == userId)
                .Select(p => new UserPictureViewModel
                {
                    PictureId = p.Id,
                    PictureFilePath = p.FilePath,
                });
        }

        public UserPictureViewModel CurrentUserPicure(string pictureId)
        {
            return picturesRepository.All()
                .Where(p => p.Id == pictureId)
                .Select(p => new UserPictureViewModel
                {
                    PictureId = p.Id,
                    PictureFilePath = p.FilePath,
                }).FirstOrDefault();
        }

        public async Task RemovePictureIdFromUserObityarysAsync(string pictureId)
        {
            var userObituarys = obituarysRepository.All()
                .Where(o => o.PictureId == pictureId);

            if (userObituarys != null)
            {
                foreach (var obituary in userObituarys)
                {
                    obituary.PictureId = null;
                }
            }

            await obituarysRepository.SaveChangesAsync();
        }
    }
}
