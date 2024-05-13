using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EnergyControlMaui.Models
{
    public class Lamp
    {
        [Key]
        public int LampId { get; set; }
        public string? LampName { get; set; }
        public string? IPAddress { get; set; }
        public int Port { get; set; }
        public bool IsConnected { get; set; }
        public bool IsOn { get; set; }
        public int Brightness { get; set; }
        public string? Color { get; set; }

        public int UserId { get; set; } 
        public User? User { get; set; }
    }
}