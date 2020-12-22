using Funeral.App.Data;
using Funeral.App.Repositories;
using Funeral.App.ViewModels;
using Funeral.Data;
using System.Collections.Generic;
using System.Linq;

namespace Funeral.App.Services
{
    public class FramesService : IFramesService
    {
        private readonly IEFRepository<Frame> framesRepository;
        private readonly IEFRepository<Obituary> obituarysRepository;

        public FramesService(
            IEFRepository<Frame> framesRepository,
            IEFRepository<Obituary> obituarysRepository)
        {
            this.framesRepository = framesRepository;
            this.obituarysRepository = obituarysRepository;
        }

        public string GetFramePathById(string frameId)
        {
            return framesRepository.All().Where(f => f.Id == frameId)
                .Select(f => f.FilePath).FirstOrDefault();
        }

        public ICollection<AllFramesViewModel> ShowAllFrames()
        {
            var frames = framesRepository.All()
                .Select(f => new AllFramesViewModel
            {
                FrameId = f.Id,
                FilePath = f.FilePath,
            }).ToList();
          
            frames.ForEach(f => f.IsUsed = IsFrameUsed(f.FrameId));
            return frames;
        }

        private bool IsFrameUsed(string frameId)
        {
            var obiturary = obituarysRepository.All()
                .Where(o => o.FrameId == frameId).FirstOrDefault();

            if (obiturary != null)
            {
                return true;
            }

            return false;
        }
    }
}
