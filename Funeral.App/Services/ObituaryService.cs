using Funeral.App.Data;
using Funeral.App.ViewModels;
using Funeral.Data;
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

        public async Task SaveToDbAsync(SaveToDbInputModel input, string userId)
        {
            var frameId = db.Frames.Where(f => f.FilePath == input.Background)
                .Select(f => f.Id).FirstOrDefault();

            var textId = db.TextTemplates.Where(t => t.Text == input.MainText).Select(t => t.Id).FirstOrDefault();

            var crossId = db.Crosses.Where(c => c.FilePath == input.Cross)
                .Select(c => c.Id).FirstOrDefault();

            var pictureId = db.Pictures.Where(p => p.FilePath == input.Picture)
                .Select(p => p.Id).FirstOrDefault();

            var obituary = new Obituary
            {
                UserId = userId,
                FrameId = frameId,
                CrossId = crossId,
                PictureId = pictureId,
                AfterCrossTexts = new AfterCrossText
                {
                    Text = input.AfterCrossText,
                },
                CrossTexts = new CrossText
                {
                    Text = input.CrossText,
                },
                Froms = new From
                {
                    Text = input.FromWhere,
                },
                FullNames = new FullName
                {
                    Name = input.FullName,
                },
                Panahidas = new Panahida
                {
                    Text = input.Panahida,
                },
                Years = new Year
                {
                    Text = input.Year,
                },
            };

            if (textId == null)
            {
                var customText = new CustomText
                {
                    Text = input.MainText,
                };

                await db.CustomTexts.AddAsync(customText);
                await db.SaveChangesAsync();

                textId = customText.Id;
                obituary.CustomTextId = textId;
            }
            else
            {
                obituary.TextTemplateId = textId;
            }

            await db.Obituaries.AddAsync(obituary);
            await db.SaveChangesAsync();
        }
    }
}
