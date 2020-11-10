using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class UploadFrameFilesViewModel
    {
        public string UploadMessage { get; set; }

        public ICollection<AllFramesViewModel> AllFrames { get; set; }
    }
}
