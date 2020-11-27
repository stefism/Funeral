using Funeral.App.ViewModels;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface IObituaryService
    {
        public Task SaveToDbAsync(SaveToDbInputModel input, string userId);
    }
}
