using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public interface ITextsService
    {
        string ReturnFirtsText();

        string GetTextById(string textId);

        Task AddTextToDbAsync(string textToAdd);

        ICollection<AllTextsViewModel> ShowAllTexts();
    }
}
