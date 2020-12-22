using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class TextsService : ITextsService
    {
        private readonly IEFRepository<TextTemplate> textTemplateRepository;
        private readonly IEFRepository<Obituary> obituarysRepository;

        public TextsService(
            IEFRepository<TextTemplate> textTemplateRepository,
            IEFRepository<Obituary> obituarysRepository)
        {
            this.textTemplateRepository = textTemplateRepository;
            this.obituarysRepository = obituarysRepository;
        }
        //TODO: ImplementAsync
        public async Task AddTextToDbAsync(string textToAdd)
        {
            var text = new TextTemplate
            {
                Text = textToAdd
            };

            await textTemplateRepository.AddAsync(text);
            await textTemplateRepository.SaveChangesAsync();
        }

        public async Task DeleteTextTemplateAsync(string textTemplateId)
        {
            var textTemplate = textTemplateRepository.All()
                .Where(t => t.Id == textTemplateId).FirstOrDefault();
            
            textTemplateRepository.Delete(textTemplate);
            await textTemplateRepository.SaveChangesAsync();
        }

        public async Task EditTextTemplateAsync(string textId, string editedText)
        {
            var text = textTemplateRepository.All()
                .Where(t => t.Id == textId).FirstOrDefault();
            text.Text = editedText;

            await textTemplateRepository.SaveChangesAsync();
        }

        public string GetTextById(string textId)
        {

            return textTemplateRepository.All()
                .Where(t => t.Id == textId)
                .Select(t => t.Text).FirstOrDefault();
        }

        public string ReturnFirtsText()
        {
            return textTemplateRepository.All().Select(x => x.Text).FirstOrDefault();
        }

        public ICollection<TextTemplateViewModel> ShowAllTexts()
        {
            var texts = textTemplateRepository.All()
                .Select(t => new TextTemplateViewModel
                {
                    TextId = t.Id,
                    TextTemplate = t.Text
                }).ToList();

            texts.ForEach(t => t.IsTextUsed = IsTextUsed(t.TextId));

            return texts;
        }

        private bool IsTextUsed(string textTemplateId)
        {
            var obiturary = obituarysRepository.All()
                .Where(o => o.TextTemplateId == textTemplateId).FirstOrDefault();

            if (obiturary != null)
            {
                return true;
            }

            return false;
        }
    }
}
