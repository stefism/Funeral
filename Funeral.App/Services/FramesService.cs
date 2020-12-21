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

        public FramesService(IEFRepository<Frame> framesRepository)
        {
            this.framesRepository = framesRepository;
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
                FilePath = f.FilePath
            }).ToList();

            return frames;
        }
    }
}
