using Funeral.App.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IObituaryService
    {
        public Task<string> SaveToDbAsync(SaveToDbInputModel input, string userId);

        public Task DeleteCurrentUserObituaryAsync(string id);

        public Task ChangeUserPictureAsync(string obituaryId, string pictureId);

        public Task<CurrentObituaryViewModel> GetCurrentAsync(string id);

        public Task<IEnumerable<CurrentObituaryViewModel>>
            AllUserObituarysAsync(string userId);
    }
}
