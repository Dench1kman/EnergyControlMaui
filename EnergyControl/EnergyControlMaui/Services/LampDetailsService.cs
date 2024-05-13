﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.