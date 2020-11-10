using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.Services
{
    public interface ICrossesService
    {
        ICollection<AllCrossesViewModel> ShowAllCrosses();
    }
}
