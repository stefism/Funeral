using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class UploadCrossesFilesViewModel
    {
        public string UploadMessage { get; set; }

        public ICollection<AllCrossesViewModel> AllCrosses { get; set; }
    }
}
