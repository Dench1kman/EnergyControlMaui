﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Models
{
    public class WifiNetwork
    {
        public string? SSID { get; set; }
        public  string? BSSID { get; set; }
        public int SignalStrength { get; set; }
        public  string? Authentication { get; set; }
        public string? Password { get; set; }
    }
}
