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

        public ICollection<AllTextsViewModel> ShowAllTexts()
        {
            var texts = db.TextTemplates.Select(t => new AllTextsViewModel
            {
                TextTemplate = t.Text
            }).ToList();

            return texts;
        }
    }
}
