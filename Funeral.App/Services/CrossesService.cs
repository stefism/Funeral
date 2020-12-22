using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class CrossesService : ICrossesService
    {
        private readonly IEFRepository<Cross> crossesRepository;
        private readonly IEFRepository<Obituary> obituaryRepository;

        public CrossesService(
            IEFRepository<Cross> crossesRepository,
            IEFRepository<Obituary> obituaryRepository)
        {
            this.crossesRepository = crossesRepository;
            this.obituaryRepository = obituaryRepository;
        }

        public async Task<string> GetCrossPathByIdAsync(string crossId)
        {
            return await crossesRepository.All()
                .Where(c => c.Id == crossId)
                .Select(c => c.FilePath).FirstOrDefaultAsync();
        }

        public ICollection<CrossViewModel> ShowAllCrosses()
        {
            var crosses = crossesRepository.All()
                .Select(c => new CrossViewModel
                {
                    CrossId = c.Id,
                    FilePath = c.FilePath
                }).ToList();

            crosses.ForEach(c => c.IsUsed = IsCrossUsed(c.CrossId));

            return crosses;
        }

        private bool IsCrossUsed(string crossId)
        {
            var obiturary = obituaryRepository.All()
                .Where(o => o.CrossId == crossId).FirstOrDefault();

            if (obiturary != null)
            {
                return true;
            }

            return false;
        }
    }
}
