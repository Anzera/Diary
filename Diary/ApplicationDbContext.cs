using Diary.Models.Configurations;
using Diary.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;
using Diary.Properties;
using Diary.Models;
using System.Runtime.InteropServices;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        //"Server=(local)\\SQLEXPRESS;Database=Diary;User Id=afraczektraining;Password=123;"
        private static string _conn = $@"Server={Settings.Default.ServerAdres}\{Settings.Default.ServerName};
                                        Database={Settings.Default.DbName};
                                        User Id={Settings.Default.User};
                                        Password={Settings.Default.Password};";
        public ApplicationDbContext()
            : base(_conn)
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