using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class UploadCrossesFilesViewModel
    {
        public string UploadMessage { get; set; }

        public ICollection<CrossViewModel> AllCrosses { get; set; }
    }
}
