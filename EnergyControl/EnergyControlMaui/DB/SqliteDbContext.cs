using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;


namespace EnergyControlMaui.DB
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lamp> Lamps { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("EnergyControl.db")}";
            optionsBuilder.UseSqlite(connectionDb);
        }
    }
}

