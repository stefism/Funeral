using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Funeral.App.Data
{
    public class FullName
    {
        public FullName()
        {
            Id = Guid.NewGuid().ToString();
            Obituaries = new HashSet<Obituary>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Obituary> Obituaries { get; set; }
    }
}
