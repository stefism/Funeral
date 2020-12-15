using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface ICrossesService
    {
        Task<string> GetCrossPathByIdAsync(string crossId);

        ICollection<AllCrossesViewModel> ShowAllCrosses();
    }
}
