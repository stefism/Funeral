using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.Services
{
    public interface ITextsService
    {
        void AddTextToDb(string textToAdd);

        ICollection<AllTextsViewModel> ShowAllTexts();
    }
}
