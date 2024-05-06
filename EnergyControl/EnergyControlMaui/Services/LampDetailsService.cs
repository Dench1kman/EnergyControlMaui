using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyControlMaui.Models;

namespace EnergyControlMaui.Services
{
    public static class LampDetailsService
    {
        private static Lamp _lamp;

        public static Lamp GetLampDetails()
        {
            return _lamp;
        }

        public static void SetLampDetails(Lamp lamp)
        {
            _lamp = lamp;
        }
    }
}
