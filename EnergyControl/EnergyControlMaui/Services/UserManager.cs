using Npgsql;
using EnergyControlMaui.Models;
using Microsoft.Extensions.Configuration;

namespace EnergyControlMaui.Services
{
    public class UserManager
    {
        private readonly string _connectionString;

        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool RegisterUser(User user)
        {
            if (_connectionString != null)
            {
                user.Password = PasswordHasher.HashPassword(user.Password);

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (FirstName, LastName, Email, Password) " +
                                   "VALUES (@FirstName, @LastName, @Email, @Password)";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Database connection string is missing");
            }
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            if (_connectionString != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync(); 

                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        object? result = await command.ExecuteScalarAsync();
                        if (result == null || result == DBNull.Value)
                        {
                            return false;
                        }

                        int userCount = Convert.ToInt32(result);

                        return userCount > 0;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Database connection string is missing");
            }

        }

        public async Task<bool> CheckPasswordAsync(string email, string hashedPassword)
        {
            if (_connectionString != null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT Password FROM Users WHERE Email = @Email";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        object? result = await command.ExecuteScalarAsync();
                        if (result == null || result == DBNull.Value)
                        {
                            return false;
                        }

                        string hashedPasswordFromDb = (string)result;

                        return hashedPassword == hashedPasswordFromDb;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Database connection string is missing");
            }
        }
    }
}

