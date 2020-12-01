using Funeral.App.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IObituaryService
    {
        public Task<string> SaveToDbAsync(SaveToDbInputModel input, string userId);

        public CurrentObituaryViewModel GetCurrent(string id);

        public IEnumerable<CurrentObituaryViewModel> AllUserObituarys(string userId);
    }
}
