using Funeral.App.ViewModels;
using Funeral.Data;
using System.Collections.Generic;
using System.Linq;

namespace Funeral.App.Services
{
    public class CrossesService : ICrossesService
    {
        private readonly ApplicationDbContext db;

        public CrossesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetCrossPathById(string crossId)
        {
            return db.Crosses.Where(c => c.Id == crossId)
                .Select(c => c.FilePath).FirstOrDefault();
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
