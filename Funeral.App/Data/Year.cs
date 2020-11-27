using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.Data
{
    public class Year
    {
        public Year()
        {
            Id = Guid.NewGuid().ToString();
            Obituaries = new HashSet<Obituary>();
        }

        [Key]
        public string Id { get; set; }

        public string Text { get; set; }

        public virtual ICollection<Obituary> Obituaries { get; set; }
    }
}
