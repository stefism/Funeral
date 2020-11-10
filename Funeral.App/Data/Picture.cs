using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.Data
{
    public class Picture
    {
        public Picture()
        {
            Id = Guid.NewGuid().ToString();
            Obituaries = new HashSet<Obituary>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string FilePath { get; set; }

        public virtual ICollection<Obituary> Obituaries { get; set; }
    }
}
