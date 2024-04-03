//using EnergyControlMaui.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//namespace EnergyControlMaui.Services
//{
//    public class AppDbContext : DbContext
//    {
//        private readonly string _connectionString;

//        public AppDbContext(string connectionString)
//        {
//            _connectionString = connectionString;
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//            options.UseNpgsql(_connectionString);
//        }

//        public DbSet<User> Users { get; set; }
//    }
//}

using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;
using EnergyControlMaui.Utilities;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace EnergyControlMaui.Services
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionDb = $"Filename={PathDB.GetPath("User.db")}";
            optionsBuilder.UseSqlite(connectionDb);
        }
    }
}

