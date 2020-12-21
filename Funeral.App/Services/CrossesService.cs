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
    public class CrossesService : ICrossesService
    {     
        private readonly IEFRepository<Cross> crossesRepository;

        public CrossesService(IEFRepository<Cross> crossesRepository)
        {           
            this.crossesRepository = crossesRepository;
        }

        public async Task<string> GetCrossPathByIdAsync(string crossId)
        {
            return await crossesRepository.All()
                .Where(c => c.Id == crossId)
                .Select(c => c.FilePath).FirstOrDefaultAsync();                
        }

        public ICollection<AllCrossesViewModel> ShowAllCrosses()
        {
            var crosses = crossesRepository.All()
                .Select(c => new AllCrossesViewModel
            {
                CrossId = c.Id,
                FilePath = c.FilePath
            }).ToList();
            return crosses;
        }
    }
}
