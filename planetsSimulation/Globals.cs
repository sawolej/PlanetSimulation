using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace planetsSimulation
{
    public class Globals
    {
        public static double G = 6.6743E-11;

        public static Dictionary<string, (double semiMajorAxis, double eccentricity, double orbitalSpeed, double inclination, double trueAnomaly, double mass, Color color)> planetOrbitalElements = new Dictionary<string, (double, double, double, double, double, double, Color)>
        {
            { "Mercury", (0.387, 0.206, 4.74E4, 7.005, 174.796, 3.285E23, Colors.Gray) },
            { "Venus", (0.723, 0.007, 3.50E4, 3.395, 50.115, 4.867E24, Colors.Orange) },
            { "Earth", (1, 0.017, 2.98E4, 0.0, 102.947, 5.97E24, Colors.Blue) },
            { "Mars", (1.524, 0.093, 2.41E4, 1.850, 336.060, 6.39E23, Colors.Red) },
            { "Jupiter", (5.203, 0.048, 1.31E4, 1.303, 14.753, 1.898E27, Colors.BurlyWood) },
            { "Saturn", (9.537, 0.054, 9.69E3, 2.489, 92.431, 5.683E26, Colors.Gold) },
            { "Uranus", (19.191, 0.047, 6.81E3, 0.772, 170.964, 8.681E25, Colors.LightBlue) },
            { "Neptune", (30.069, 0.009, 5.43E3, 1.769, 44.971, 1.024E26, Colors.BlueViolet) }
        };


    }
}
