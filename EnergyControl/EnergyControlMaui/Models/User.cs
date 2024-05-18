using System.ComponentModel.DataAnnotations;


namespace EnergyControlMaui.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public Lamp? Lamps { get; set; }
    }
}
