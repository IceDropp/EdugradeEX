using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdugradeEX
{
    public class FloorHeatingData
    {
        public double MassFlow { get; set; } // Massflöde i kg/s
        public double TemperatureOutside { get; set; } // Utomhustemperatur i °C
        public double TemperatureInside { get; set; } // Innetemperatur i °C
        public double Insulation { get; set; } // Isoleringens kvalitet (0-1)
        public double Cost { get; set; } // Kostnad per kWh
    }
}