using Funeral.App.Data;
using Funeral.App.ViewModels;
using Funeral.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class ObituaryService : IObituaryService
    {
        private readonly ApplicationDbContext db;

        public ObituaryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task DeleteCurrentUserObituaryAsync(string id)
        {
            var obituary = db.Obituaries.Where(o => o.Id == id).FirstOrDefault();

            var userObituarys = db.UserObituaries
                .Where(x => x.ObituaryId == id).FirstOrDefault();

            db.UserObituaries.Remove(userObituarys);
            db.Obituaries.Remove(obituary);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurrentObituaryViewModel>> AllUserObituarysAsync(string userId)
        {
            var all = await db.Obituaries.Where(o => o.UserId == userId)
                .Select(o => new CurrentObituaryViewModel
                {
                    Id = o.Id,
                    Background = o.Frame.FilePath,
                    Cross = o.Cross.FilePath,
                    CrossText = o.CrossTexts.Text,
                    AfterCrossText = o.AfterCrossTexts.Text,
                    Picture = o.Picture.FilePath,
                    FullName = o.FullNames.Name,
                    Year = o.Years.Text,
                    MainText = o.TextTemplate.Text ?? o.CustomText.Text,
                    Panahida = o.Panahidas.Text,
                    FromWhere = o.Froms.Text,
                }).ToListAsync();

            return all;
        }

        public async Task<CurrentObituaryViewModel> GetCurrentAsync(string id)
        {
            var current = await db.Obituaries.Where(o => o.Id == id)
                .Select(o => new CurrentObituaryViewModel
                {
                    Id = o.Id,
                    Background = o.Frame.FilePath,
                    Cross = o.Cross.FilePath,
                    CrossText = o.CrossTexts.Text,
                    AfterCrossText = o.AfterCrossTexts.Text,
                    Picture = o.Picture.FilePath,
                    FullName = o.FullNames.Name,
                    Year = o.Years.Text,
                    MainText = o.TextTemplate.Text ?? o.CustomText.Text,
                    Panahida = o.Panahidas.Text,
                    FromWhere = o.Froms.Text,
                }).FirstOrDefaultAsync();

            return current;
        }

        public async Task<string> SaveToDbAsync(SaveToDbInputModel input, string userId)
        {
            var frameId = db.Frames.Where(f => f.FilePath == input.Background)
                .Select(f => f.Id).FirstOrDefault();            

            var crossId = db.Crosses.Where(c => c.FilePath == input.Cross)
                .Select(c => c.Id).FirstOrDefault();

            var pictureId = db.Pictures.Where(p => p.FilePath == input.Picture)
                .Select(p => p.Id).FirstOrDefault();

            var crossText = db.CrossTexts
                .Where(ct => ct.Text == input.CrossText)
                .FirstOrDefault();
            
            var afterCrossText = db.AfterCrossTexts
                .Where(act => act.Text == input.AfterCrossText)
                .FirstOrDefault();

            var fullName = db.FullNames.Where(fn => fn.Name == input.FullName)
                .FirstOrDefault();

            var year = db.Years.Where(y => y.Text == input.Year).FirstOrDefault();

            var textTemplate = db.TextTemplates.Where(t => t.Text == input.MainText).FirstOrDefault();

            var panahida = db.Panahidas.Where(p => p.Text == input.Panahida).FirstOrDefault();

            var from = db.Froms.Where(f => f.Text == input.FromWhere).FirstOrDefault();

            var obituary = new Obituary
            {
                UserId = userId,
                FrameId = frameId,
                CrossId = crossId,
                PictureId = pictureId,              
            };

            if (afterCrossText == null)
            {
                afterCrossText = new AfterCrossText
                {
                    Text = input.AfterCrossText
                };
            }
            obituary.AfterCrossTexts = afterCrossText;

            if (crossText == null)
            {
                crossText = new CrossText
                {
                    Text = input.CrossText
                };
            }
            obituary.CrossTexts = crossText;

            if (fullName == null)
            {
                fullName = new FullName
                {
                    Name = input.FullName
                };
            }
            obituary.FullNames = fullName;

            if (year == null)
            {
                year = new Year
                {
                    Text = input.Year
                };
            }
            obituary.Years = year;

            if (textTemplate == null)
            {
                var customText = new CustomText
                {
                    Text = input.MainText
                };
                obituary.CustomText = customText;
            }
            else
            {
                obituary.TextTemplate = textTemplate;
            }

            if (panahida == null)
            {
                panahida = new Panahida
                {
                    Text = input.Panahida
                };
            }
            obituary.Panahidas = panahida;

            if (from == null)
            {
                from = new From
                {
                    Text = input.FromWhere
                };
            }
            obituary.Froms = from;

            var userObituary = new UserObituary
            {
                Obituary = obituary,
                UserId = userId
            };

            await db.Obituaries.AddAsync(obituary);
            await db.UserObituaries.AddAsync(userObituary);
            
            await db.SaveChangesAsync();

            return obituary.Id;
        } 
    }
}
