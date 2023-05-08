using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

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
                Name = "Mercury",
                Mass = 3.285E23,
                Color = System.Windows.Media.Colors.Gray,
                Position = new System.Windows.Media.Media3D.Point3D(0.387, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 4.79E4, 0)
            });

            planets.Add(new Planet
            {
                Name = "Venus",
                Mass = 4.867E24,
                Color = System.Windows.Media.Colors.Orange,
                Position = new System.Windows.Media.Media3D.Point3D(0.723, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 3.50E4, 0)
            });

            planets.Add(new Planet
            {
                Name = "Earth",
                Mass = 5.97E24,
                Color = System.Windows.Media.Colors.Blue,
                Position = new System.Windows.Media.Media3D.Point3D(1, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 2.98E4, 0)
            });

            planets.Add(new Planet
            {
                Name = "Mars",
                Mass = 6.39E23,
                Color = System.Windows.Media.Colors.Red,
                Position = new System.Windows.Media.Media3D.Point3D(1.524, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 2.41E4, 0)
            });

            planets.Add(new Planet
            {
                Name = "Jupiter",
                Mass = 1.898E27,
                Color = System.Windows.Media.Colors.BurlyWood,
                Position = new System.Windows.Media.Media3D.Point3D(5.203, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 1.31E4, 0)
            });

            planets.Add(new Planet
            {
                Name = "Saturn",
                Mass = 5.683E26,
                Color = System.Windows.Media.Colors.Gold,
                Position = new System.Windows.Media.Media3D.Point3D(9.539, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 9.69E3, 0)
            });

            planets.Add(new Planet
            {
                Name = "Uranus",
                Mass = 8.681E25,
                Color = System.Windows.Media.Colors.LightBlue,
                Position = new System.Windows.Media.Media3D.Point3D(19.18, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 6.81E3, 0)
            });

            planets.Add(new Planet
            {
                Name = "Neptune",
                Mass = 1.024E26,
                Color = System.Windows.Media.Colors.BlueViolet,
                Position = new System.Windows.Media.Media3D.Point3D(30.07, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 5.43E3, 0)
            });

            // Add more planets...

            return planets;
        }

        public static void UpdatePlanetPosition(Planet planet, List<Planet> planets, double timeStep)
        {
            // Compute the gravitational force on the planet
            Vector3D force = ComputeNetForceOnPlanet(planet, planets);

            // Compute the planet's acceleration
            Vector3D acceleration = force / planet.Mass;

            // Update the planet's velocity based on its acceleration
            planet.Velocity += acceleration * timeStep;
            Debug.WriteLine($"Update {planet.Name} with velocity {planet.Velocity}.");
            // Update the planet's position based on its velocity
            Debug.WriteLine($"Update {planet.Name} with old x {planet.Position.X}.");
            Debug.WriteLine($"Update {planet.Name} with old y {planet.Position.Y}.");
            Debug.WriteLine($"Update {planet.Name} with old z {planet.Position.Z}.");
            planet.Position = new Point3D(
                planet.Position.X + planet.Velocity.X * timeStep,
                planet.Position.Y + planet.Velocity.Y * timeStep,
                planet.Position.Z + planet.Velocity.Z * timeStep
            );
            Debug.WriteLine($"Update {planet.Name} with NEW x {planet.Position.X}.");
            Debug.WriteLine($"Update {planet.Name} with NEW y {planet.Position.Y}.");
            Debug.WriteLine($"Update {planet.Name} with NEW z {planet.Position.Z}.");
        }

        public static Vector3D ComputeNetForceOnPlanet(Planet planet, List<Planet> planets)
        {
            Vector3D netForce = new Vector3D(0, 0, 0);

            foreach (Planet otherPlanet in planets)
            {
                if (otherPlanet != planet)
                {
                    Vector3D direction = otherPlanet.Position - planet.Position;
                    double distance = direction.Length;
                    direction.Normalize();

                    double forceMagnitude = (Globals.G * planet.Mass * otherPlanet.Mass) / (distance * distance);
                    Vector3D force = direction * forceMagnitude;

                    netForce += force;
                }
            }

            return netForce;
        }
    }
}
