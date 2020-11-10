using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class MakeItViewModel
    {
        public string CurrentFrame { get; set; }

        public ICollection<AllFramesViewModel> AllFrames { get; set; }
    }
}
