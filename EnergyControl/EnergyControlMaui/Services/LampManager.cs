#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;
using EnergyControlMaui.DB;


namespace EnergyControlMaui.Services
{
    public class LampManager
    {
        public Lamp? Lamp { get; private set; }
        private static LampManager? _instance;
        private readonly SqliteDbContext _db;

        private LampManager()
        {
            _db = new SqliteDbContext();
        }

        public static LampManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LampManager();
            }
            return _instance;
        }

        public void SetDetails(Lamp lamp)
        {
            Lamp = lamp;
        }

        public Lamp GetDetails()
        {
            return Lamp;
        }

        public bool AddLamp(Lamp lamp)
        {
            _db.Lamps.Add(lamp);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex) 
            {
                Console.WriteLine($"Error while saving changes: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException}");
                 return false;
            }
        }

        public async Task<Lamp> GetLampByIdAsync(int lampId)
        {
            return await _db.Lamps.FirstOrDefaultAsync(l => l.LampId == lampId);
        }

        public async Task<Lamp> GetLampByUserIdAsync(int userId)
        {
            return await _db.Lamps.FirstOrDefaultAsync(l => l.UserId == userId);
        }

        public async Task<bool> UpdateLampAsync(Lamp updatedLamp)
        {
            var existingLamp = await _db.Lamps.FirstOrDefaultAsync(l => l.LampId == updatedLamp.LampId);
            if (existingLamp != null)
            {
                existingLamp.LampName = updatedLamp.LampName;
                existingLamp.IPAddress = updatedLamp.IPAddress;
                existingLamp.Port = updatedLamp.Port;
                existingLamp.IsConnected = updatedLamp.IsConnected;
                existingLamp.IsOn = updatedLamp.IsOn;
                existingLamp.Brightness = updatedLamp.Brightness;
                existingLamp.Color = updatedLamp.Color;

                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveLampAsync(int lampId)
        {
            var lampToRemove = await _db.Lamps.FirstOrDefaultAsync(l => l.LampId == lampId);
            if (lampToRemove != null)
            {
                _db.Lamps.Remove(lampToRemove);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

#pragma warning restore CS8603 // Possible null reference return.