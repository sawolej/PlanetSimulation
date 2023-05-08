using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planetsSimulation
{
    public class Planet
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public System.Windows.Media.Color Color { get; set; }
        public System.Windows.Media.Media3D.Point3D Position { get; set; }
        public System.Windows.Media.Media3D.Vector3D Velocity { get; set; }

        // Add any other properties and methods as needed
    }

    public static class PlanetSimulator
    {
        public static List<Planet> InitializePlanets()
        {
            List<Planet> planets = new List<Planet>();

            // Add planets to the list, for example:
            planets.Add(new Planet
            {
                Name = "Earth",
                Mass = 5.97E24,
                Color = System.Windows.Media.Colors.Blue,
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 0, 0)
            });

            // Add more planets...

            return planets;
        }

        public static void UpdatePlanets(List<Planet> planets, double timeDelta)
        {
            // Update the positions and velocities of the planets based on the simulation logic

            // For example:
            // 1. Compute gravitational forces between planets
            // 2. Update velocities based on the forces
            // 3. Update positions based on the velocities
        }
    }
}
