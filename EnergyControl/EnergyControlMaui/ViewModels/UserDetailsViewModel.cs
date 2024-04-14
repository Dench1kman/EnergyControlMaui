using EnergyControlMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.ViewModels
{
    public class UserDetailsViewModel
    {
        public  User CurrentUser { get; set; }
        public UserDetailsViewModel(User user) 
        {
            CurrentUser = user;
        }
    }
}
