using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace FullStackTest.Data
{

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
        public DbSet<User> User { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<Language> Language { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData
                    (new Language { Id = 1, Title = "Urdu" },
                    new Language { Id = 2, Title = "English" },
                    new Language { Id = 3, Title = "Arabic" });
            modelBuilder.Entity<UserLanguage>()
                .HasKey(x => new { x.UserId, x.LanguageId });
            modelBuilder.Entity<UserLanguage>()
                .HasOne(x => x.User)
                .WithMany(x => x.Languages)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserLanguage>()
                .HasOne(x => x.Language)
                .WithMany(x => x.Languages)
                .HasForeignKey(bc => bc.LanguageId);
        }
    }

}
