using Funeral.App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class MakeItViewModel
    {
        public string CurrentFrame { get; set; }

        public string CurrentCross { get; set; }

        public string CurrentText { get; set; }

        public string CrossText { get; set; }

        public string AfterCrossText { get; set; }

        public string FullName { get; set; }

        public string Year { get; set; }

        public string Panahida { get; set; }

        public string From { get; set; }

        public Elements Elements { get; set; }

        public ICollection<AllFramesViewModel> AllFrames { get; set; }

        public ICollection<AllCrossesViewModel> AllCrosses { get; set; }

        public ICollection<AllTextsViewModel> AllTexts { get; set; }
    }
}
