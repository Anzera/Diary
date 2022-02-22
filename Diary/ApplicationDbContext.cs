using Diary.Models.Configurations;
using Diary.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;
using Diary.Properties;
using Diary.Models;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        //public ConnectionStringSettings conn = new ConnectionStringSettings();
        public ApplicationDbContext()
            : base(UserSettings.ConnectingStringBuilder())
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }

    }
}