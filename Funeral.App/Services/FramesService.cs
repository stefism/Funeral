using Funeral.App.ViewModels;
using Funeral.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.App.Services
{
    public class FramesService : IFramesService
    {
        private readonly ApplicationDbContext db;

        public FramesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetFramePathById(string frameId)
        {
            return db.Frames.Where(f => f.Id == frameId)
                .Select(f => f.FilePath).FirstOrDefault();
        }

        public ICollection<AllFramesViewModel> ShowAllFrames()
        {
            var frames = db.Frames.Select(f => new AllFramesViewModel
            {
                FrameId = f.Id,
                FilePath = f.FilePath
            }).ToList();

            return frames;
        }
    }
}
