﻿using Funeral.App.Data;
using Funeral.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funeral.App.Services
{
    public interface IFramesService
    {
        ICollection<FrameViewModel> ShowAllFrames();

        string GetFramePathById(string frameId);
        
    }
}
