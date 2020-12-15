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
        private readonly ApplicationDbContext db;

        public CrossesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<string> GetCrossPathByIdAsync(string crossId)
        {
            return await db.Crosses.Where(c => c.Id == crossId)
                .Select(c => c.FilePath).FirstOrDefaultAsync();
        }

        public ICollection<AllCrossesViewModel> ShowAllCrosses()
        {
            var crosses = db.Crosses.Select(c => new AllCrossesViewModel
            {
                CrossId = c.Id,
                FilePath = c.FilePath
            }).ToList();

            return crosses;
        }
    }
}
