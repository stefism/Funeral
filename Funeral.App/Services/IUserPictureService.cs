using Funeral.App.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IUserPictureService
    {
        int GetUserPictureCount(string userId);

        public UserPictureViewModel CurrentUserPicure(string pictureId);

        IEnumerable<UserPictureViewModel> AllUserPictures(string userId);

        public Task RemovePictureIdFromUserObityarysAsync(string pictureId);
    }
}
