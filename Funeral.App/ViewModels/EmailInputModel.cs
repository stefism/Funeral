using Funeral.App.GlobalConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.ViewModels
{
    public class EmailInputModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredField)]
        [MaxLength(100)]
        [Display(Name = "Относно:")]
        public string Subject { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredField)]
        [MaxLength(1000)]
        [Display(Name = "Съобщение:")]
        public string Message { get; set; }
    }
}
