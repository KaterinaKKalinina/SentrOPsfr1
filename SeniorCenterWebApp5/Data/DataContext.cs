using Microsoft.EntityFrameworkCore;
using SeniorCenterWebApp.Models;

namespace SeniorCenterWebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        

        public DbSet<User> Users { get; set; }
        public DbSet<TestResult> TestResults{ get; set; }
        //public DbSet<Activity> Activities { get; set; } //не надо
        //public DbSet<UserActivities> UserActivities { get; set; }// не надо
        public DbSet<Meropr> Meroprs { get; set; }
        public DbSet<UserMeropr> UserMeroprs { get; set; }
        public DbSet<New> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMeropr>()
                .HasIndex(x => new { x.UserId, x.MeroprId })
                .IsUnique();
        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        // Определяем, какой провайдер используется
        public bool IsNpgsql => Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL";

    }
}
