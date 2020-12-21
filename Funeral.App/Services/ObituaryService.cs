using Funeral.App.Data;
using Funeral.App.Repositories;
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
        private readonly IEFRepository<Obituary> obituaryRepository;
        private readonly IEFRepository<UserObituary> userObituaryRepository;
        private readonly IEFRepository<Picture> pictureRepository;
        private readonly IEFRepository<Cross> crossesRepository;
        private readonly IEFRepository<CrossText> crossTextsRepository;
        private readonly IEFRepository<CustomText> customTextsRepository;
        private readonly IEFRepository<AfterCrossText> afterCrossTextsRepository;
        private readonly IEFRepository<TextTemplate> textTemplatesRepository;
        private readonly IEFRepository<Frame> framesRepository;
        private readonly IEFRepository<From> fromsRepository;
        private readonly IEFRepository<FullName> fullNamesRepository;
        private readonly IEFRepository<Panahida> panahidasRepository;
        private readonly IEFRepository<Year> yearsRepository;

        public ObituaryService(
            ApplicationDbContext db,
            IEFRepository<Obituary> obituaryRepository,
            IEFRepository<UserObituary> userObituaryRepository,
            IEFRepository<Picture> pictureRepository,
            IEFRepository<Cross> crossesRepository,
            IEFRepository<CrossText> crossTextsRepository,
            IEFRepository<CustomText> customTextsRepository,
            IEFRepository<AfterCrossText> afterCrossTextsRepository,
            IEFRepository<TextTemplate> textTemplatesRepository,
            IEFRepository<Frame> framesRepository,
            IEFRepository<From> fromsRepository,
            IEFRepository<FullName> fullNamesRepository,
            IEFRepository<Panahida> panahidasRepository,
            IEFRepository<Year> yearsRepository
            )
        {
            this.db = db;
            this.obituaryRepository = obituaryRepository;
            this.userObituaryRepository = userObituaryRepository;
            this.pictureRepository = pictureRepository;
            this.crossesRepository = crossesRepository;
            this.crossTextsRepository = crossTextsRepository;
            this.customTextsRepository = customTextsRepository;
            this.afterCrossTextsRepository = afterCrossTextsRepository;
            this.textTemplatesRepository = textTemplatesRepository;
            this.framesRepository = framesRepository;
            this.fromsRepository = fromsRepository;
            this.fullNamesRepository = fullNamesRepository;
            this.panahidasRepository = panahidasRepository;
            this.yearsRepository = yearsRepository;
        }

        public async Task DeleteCurrentUserObituaryAsync(string id)
        {
            var obituary = obituaryRepository.All().Where(o => o.Id == id).FirstOrDefault();

            var userObituarys = userObituaryRepository.All()
                .Where(x => x.ObituaryId == id).FirstOrDefault();

            userObituaryRepository.Delete(userObituarys);

            obituaryRepository.Delete(obituary);

            await obituaryRepository.SaveChangesAsync();
        }

        public async Task ChangeUserPictureAsync(string obituaryId, string pictureId)
        {
            var obituary = obituaryRepository.All().FirstOrDefault(x => x.Id == obituaryId);

            var picture = pictureRepository.All().FirstOrDefault(x => x.Id == pictureId);

            obituary.Picture = picture;

            await pictureRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurrentObituaryViewModel>> AllUserObituarysAsync(string userId)
        {
            var all = await obituaryRepository.All().Where(o => o.UserId == userId)
                .Select(o => new CurrentObituaryViewModel
                {
                    ObituaryId = o.Id,
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
            var current = await obituaryRepository.All().Where(o => o.Id == id)
                .Select(o => new CurrentObituaryViewModel
                {
                    ObituaryId = o.Id,
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
            var frameId = framesRepository.All().Where(f => f.FilePath == input.Background)
                .Select(f => f.Id).FirstOrDefault();

            var crossId = crossesRepository.All().Where(c => c.FilePath == input.Cross)
                .Select(c => c.Id).FirstOrDefault();

            var pictureId = pictureRepository.All().Where(p => p.FilePath == input.Picture)
                .Select(p => p.Id).FirstOrDefault();

            var crossText = crossTextsRepository.All()
                .Where(ct => ct.Text == input.CrossText)
                .FirstOrDefault();

            var afterCrossText = afterCrossTextsRepository.All()
                .Where(act => act.Text == input.AfterCrossText)
                .FirstOrDefault();

            var fullName = fullNamesRepository.All()
                .Where(fn => fn.Name == input.FullName)
                .FirstOrDefault();

            var year = yearsRepository.All().Where(y => y.Text == input.Year).FirstOrDefault();

            var textTemplate = textTemplatesRepository.All()
                .Where(t => t.Text == input.MainText).FirstOrDefault();

            var panahida = panahidasRepository.All()
                .Where(p => p.Text == input.Panahida).FirstOrDefault();

            var from = fromsRepository.All()
                .Where(f => f.Text == input.FromWhere).FirstOrDefault();

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

            await obituaryRepository.AddAsync(obituary);
            await userObituaryRepository.AddAsync(userObituary);

            await obituaryRepository.SaveChangesAsync();

            return obituary.Id;
        }
    }
}
