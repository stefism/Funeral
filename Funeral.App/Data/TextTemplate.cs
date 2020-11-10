using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.Data
{
    public class TextTemplate
    {
        public TextTemplate()
        {
            Id = Guid.NewGuid().ToString();
            Obituaries = new HashSet<Obituary>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual ICollection<Obituary> Obituaries { get; set; }
    }
}
