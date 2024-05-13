﻿using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;
using EnergyControlMaui.Utilities;


namespace EnergyControlMaui.Services
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lamp> Lamps { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("User.db")}";
            optionsBuilder.UseSqlite(connectionDb);
        }
    }
}

