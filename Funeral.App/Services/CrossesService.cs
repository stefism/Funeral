using Funeral.App.ViewModels;
using Funeral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Funeral.App.Services
{
    public class CrossesService : ICrossesService
    {
        private readonly ApplicationDbContext db;

        public CrossesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<AllCrossesViewModel> ShowAllCrosses()
        {
            var crosses = db.Crosses.Select(c => new AllCrossesViewModel
            {
                FilePath = c.FilePath
            }).ToList();

            return crosses;
        }
    }
}
