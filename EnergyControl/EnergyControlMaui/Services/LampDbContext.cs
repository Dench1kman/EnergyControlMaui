using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;
using EnergyControlMaui.Utilities;

namespace EnergyControlMaui.Services
{
    public class LampDbContext : DbContext
    {
        public DbSet<Lamp> Lamps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("User.db")}";
            optionsBuilder.UseSqlite(connectionDb);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lamp>().ToTable("Lamps");
            modelBuilder.Entity<Lamp>().HasKey(x => x.LampId);
        }
    }
}
