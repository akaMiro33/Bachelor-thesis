using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspBlog.Models;

namespace AspBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           // modelBuilder.Entity<Praca>().HasMany(i => i.feature).WithMany();
        }
        //public DbSet<AspBlog.Models.Article> Article { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskMetaData> PracaTagy{ get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

    }
}
