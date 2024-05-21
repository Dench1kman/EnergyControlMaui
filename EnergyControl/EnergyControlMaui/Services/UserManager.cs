#pragma warning disable CS8603 // Possible null reference return.

using EnergyControlMaui.DB;
using EnergyControlMaui.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyControlMaui.Services
{
    public class UserManager
    {
        public User? User { get; private set; }
        private static UserManager? _instance;
        private readonly SqliteDbContext _db;

        private UserManager()
        {
            _db = new SqliteDbContext();
        }

        public static UserManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            return _instance;
        }

        public void SetUser(User user)
        {
            User = user;
        }

        public User GetUser()
        {
            return User;
        }

        public bool RegisterUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            Console.WriteLine("\nUser successfully added!\n");
            return true;
        }

        public async Task<bool> UpdateUserAsync(User updatedUser)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.UserId == updatedUser.UserId);
            if (existingUser != null)
            {
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;
                existingUser.Lamps = updatedUser.Lamps;

                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<User> GetUserWithLampAsync(int userId)
        {
            return await _db.Users.Include(u => u.Lamps).FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _db.Users.Where(u => u.Email == email).AnyAsync();
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

        public async Task<User> GetUserDataAsync(string email)
        {
            return await _db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveLampAsync(int userId)
        {
            var user = await _db.Users.Include(u => u.Lamps).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null && user.Lamps != null)
            {
                user.Lamps = null;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }
    }
}

#pragma warning restore CS8603 // Possible null reference return.
