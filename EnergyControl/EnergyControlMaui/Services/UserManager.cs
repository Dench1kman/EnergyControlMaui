//using Npgsql;
//using EnergyControlMaui.Models;
//using Microsoft.Extensions.Configuration;
//using EnergyControlMaui.Services;
//using System;
//using System.Linq;


//namespace EnergyControlMaui.Services
//{
//    public class UserManager
//    {
//        private readonly string _connectionString;

//        public UserManager(string connectionString)
//        {
//            _connectionString = connectionString;
//        }
//        public bool RegisterUser(User user)
//        {
//            if (_connectionString != null)
//            {
//                user.Password = PasswordHasher.HashPassword(user.Password);

//                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
//                {
//                    connection.Open();

//                    string query = "INSERT INTO Users (FirstName, LastName, Email, Password) " +
//                                   "VALUES (@FirstName, @LastName, @Email, @Password)";

//                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
//                        command.Parameters.AddWithValue("@LastName", user.LastName);
//                        command.Parameters.AddWithValue("@Email", user.Email);
//                        command.Parameters.AddWithValue("@Password", user.Password);

//                        int rowsAffected = command.ExecuteNonQuery();

//                        return rowsAffected > 0;
//                    }
//                }
//            }
//            else
//            {
//                throw new InvalidOperationException("Database connection string is missing");
//            }
//        }
//public async Task<bool> UserExistsAsync(string email)
//{
//    if (_connectionString != null)
//    {
//        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

//            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Email", email);
//                object? result = await command.ExecuteScalarAsync();
//                if (result == null || result == DBNull.Value)
//                {
//                    return false;
//                }

//                int userCount = Convert.ToInt32(result);

//                return userCount > 0;
//            }
//        }
//    }
//    else
//    {
//        throw new InvalidOperationException("Database connection string is missing");
//    }

//}

//public async Task<bool> CheckPasswordAsync(string email, string hashedPassword)
//{
//    if (_connectionString != null)
//    {
//        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            string query = "SELECT Password FROM Users WHERE Email = @Email";

//            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Email", email);
//                object? result = await command.ExecuteScalarAsync();
//                if (result == null || result == DBNull.Value)
//                {
//                    return false;
//                }

//                string hashedPasswordFromDb = (string)result;

//                return hashedPassword == hashedPasswordFromDb;
//            }
//        }
//    }
//    else
//    {
//        throw new InvalidOperationException("Database connection string is missing");
//    }
//}
//    }
//}

//using system;
//using energycontrolmaui.models;
//using microsoft.entityframeworkcore;
//using system.threading.tasks;

//namespace energycontrolmaui.services
//{
//    public class usermanager
//    {
//        private readonly appdbcontext _dbcontext;

//        public usermanager(string connectionstring)
//        {
//            var optionsbuilder = new dbcontextoptionsbuilder<appdbcontext>();
//            optionsbuilder.usenpgsql(connectionstring);
//            _dbcontext = new appdbcontext(optionsbuilder.options);
//        }

//        public bool registeruser(user user)
//        {
//            try
//            {
//                user.password = passwordhasher.hashpassword(user.password);
//                _dbcontext.users.add(user);
//                _dbcontext.savechanges();
//                return true;
//            }
//            catch (exception ex)
//            {
//                обработка ошибок
//                console.writeline(ex.message);
//                return false;
//            }
//        }

//        public async task<bool> userexistsasync(string email)
//        {
//            return await _dbcontext.users.anyasync(u => u.email == email);
//        }

//        public async task<bool> checkpasswordasync(string email, string hashedpassword)
//        {
//            var user = await _dbcontext.users.firstordefaultasync(u => u.email == email);
//            if (user == null)
//            {
//                return false;
//            }

//            return user.password == hashedpassword;
//        }
//    }
//}


using EnergyControlMaui.Services;
using EnergyControlMaui.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyControlMaui.Services
{
    public class UserManager
    {
        private readonly SqliteDbContext _db;

        public UserManager(SqliteDbContext db)
        {
            _db = db;
        }

        public bool RegisterUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            Console.WriteLine("\nUser successfully added!\n");

            var users = _db.Users.ToList();
            Console.WriteLine("User List:");
            foreach (User _user in users)
                Console.WriteLine($"{_user.UserId}. {_user.FirstName}, {_user.LastName}, {_user.Email}, {_user.Password},");
            return true;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> CheckPasswordAsync(string email, string hashedPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user.Password == hashedPassword;
            }
            return false;
        }
    }
}

