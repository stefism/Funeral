using Funeral.App.Data;
using Funeral.App.ViewModels;
using Funeral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Funeral.App.Services
{
    public class TextsService : ITextsService
    {
        private readonly ApplicationDbContext db;

        public TextsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTextToDb(string textToAdd)
        {
            var text = new TextTemplate
            {
                Text = textToAdd
            };

            db.TextTemplates.Add(text);
            db.SaveChanges();
        }

        public string GetTextById(string textId)
        {
            return db.TextTemplates.Where(t => t.Id == textId).Select(t => t.Text).FirstOrDefault();
        }

        public string ReturnFirtsText()
        {
            return db.TextTemplates.Select(x => x.Text).FirstOrDefault();
        }

        public ICollection<AllTextsViewModel> ShowAllTexts()
        {
            var texts = db.TextTemplates.Select(t => new AllTextsViewModel
            {
                TextId = t.Id,
                TextTemplate = t.Text
            }).ToList();

            return texts;
        }
    }
}
