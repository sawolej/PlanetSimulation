using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
        // Semi-major axis in AU, eccentricity, and average orbital speed in m/s for each planet


        public static (Point3D position, Vector3D velocity) CalculateInitialOrbitConditions(double semiMajorAxis, double eccentricity, double orbitalSpeed)
        {
            double perihelionDistance = semiMajorAxis * (1 - eccentricity);
            Point3D position = new Point3D(perihelionDistance, 0, 0);
            Vector3D velocity = new Vector3D(0, orbitalSpeed, 0);

            return (position, velocity);
        }

        public static List<Planet> InitializePlanets()
        {
            //double theta = 0;
            List<Planet> planets = new List<Planet>();
            foreach (var entry in Globals.planetOrbitalElements)
            {
                string planetName = entry.Key;
                var (semiMajorAxis, eccentricity, orbitalSpeed, inclination, trueAnomaly, mass, color) = entry.Value;

                double r = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Math.Cos(trueAnomaly * (Math.PI / 180)));
                double phi = trueAnomaly * (Math.PI / 180);
                double i = inclination * (Math.PI / 180);

                double posX = r * (Math.Cos(phi) * Math.Cos(i));
                double posY = r * (Math.Sin(phi) * Math.Cos(i));
                double posZ = r * Math.Sin(i);

                double velX = -orbitalSpeed * Math.Sin(phi);
                double velY = orbitalSpeed * (Math.Cos(phi) + eccentricity);
                Debug.WriteLine($"Point3D({posX}, {posY}, {posZ}),");
                planets.Add(new Planet
                {
                    Name = planetName,
                    Mass = mass,
                    Color = color,
                    Position = new Point3D(posX, posY, posZ),
                    Velocity = new Vector3D(velX, velY, 0)
                });
            }
            planets.Add(new Planet
            {
                Name = "Sun",
                Mass = 1.989E30,
                Color = System.Windows.Media.Colors.Yellow,
                Position = new System.Windows.Media.Media3D.Point3D(0, 0, 0),
                Velocity = new System.Windows.Media.Media3D.Vector3D(0, 0, 0)
            });

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
