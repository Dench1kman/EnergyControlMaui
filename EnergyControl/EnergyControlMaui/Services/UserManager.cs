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

