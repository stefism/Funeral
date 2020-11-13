using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.Services
{
    public interface ITextsService
    {
        string ReturnFirtsText();

        string GetTextById(string textId);

        void AddTextToDb(string textToAdd);

        ICollection<AllTextsViewModel> ShowAllTexts();
    }
}
