using System;
using System.Collections.Generic;
using System.Text;
using Funeral.App.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Funeral.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cross> Crosses { get; set; }

        public DbSet<CustomText> CustomTexts { get; set; }

        public DbSet<Frame> Frames { get; set; }

        public DbSet<Obituary> Obituaries { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<TextTemplate> TextTemplates { get; set; }

        public DbSet<UserObituary> UserObituaries { get; set; }
    }
}
