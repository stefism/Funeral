using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.ViewModels;
using Funeral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class TextsService : ITextsService
    {
        private readonly IEFRepository<TextTemplate> textRepository;

        public TextsService(IEFRepository<TextTemplate> textRepository)
        {
            this.textRepository = textRepository;
        }
        //TODO: ImplementAsync
        public async Task AddTextToDbAsync(string textToAdd)
        {
            var text = new TextTemplate
            {
                Text = textToAdd
            };

            await textRepository.AddAsync(text);
            await textRepository.SaveChangesAsync();          
        }

        public string GetTextById(string textId)
        {

            return textRepository.All()
                .Where(t => t.Id == textId)
                .Select(t => t.Text).FirstOrDefault();                                
        }

        public string ReturnFirtsText()
        {
            return textRepository.All().Select(x => x.Text).FirstOrDefault();           
        }

        public ICollection<AllTextsViewModel> ShowAllTexts()
        {
            var texts = textRepository.All()
                .Select(t => new AllTextsViewModel
            {
                TextId = t.Id,
                TextTemplate = t.Text
            }).ToList();

            return texts;
        }
    }
}
