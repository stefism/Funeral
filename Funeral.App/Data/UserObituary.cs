using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Funeral.App.Data
{
    public class UserObituary
    {
        public UserObituary()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ObituaryId { get; set; }

        public virtual Obituary Obituary { get; set; }
    }
}
