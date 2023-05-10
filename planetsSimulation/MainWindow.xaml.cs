using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Threading;

namespace planetsSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ModelVisual3D CreatePlanetModel(Planet planet)
        {
            // Create a sphere geometry for the planet
            MeshBuilder meshBuilder = new MeshBuilder();
            meshBuilder.AddSphere(planet.Position, 100); // Change 0.5 to the desired planet size

            // Create the planet material (color, texture, etc.)
            DiffuseMaterial planetMaterial = new DiffuseMaterial(new SolidColorBrush(planet.Color));

            // Create the transform for the planet
            TranslateTransform3D planetTransform = new TranslateTransform3D(planet.Position.X, planet.Position.Y, planet.Position.Z);

            // Create and return the planet 3D model
            return new ModelVisual3D
            {
                Content = new GeometryModel3D(meshBuilder.ToMesh(), planetMaterial)
                {
                    Transform = planetTransform
                }
            };
        }

        public MainWindow()
        {
            InitializeComponent();

            List<Planet> planets = PlanetSimulator.InitializePlanets();
            Debug.WriteLine("Planets initialized.");

            foreach (Planet planet in planets)
            {
                Debug.WriteLine($"Added {planet.Name} to the viewport.");
                ModelVisual3D planetModel = CreatePlanetModel(planet);
                viewport3D.Children.Add(planetModel);
            }

            // Start the simulation loop
            Task.Run(() =>
            {
                while (true)
                {
                    // Update the positions of all planets
                   /* foreach (Planet planet in planets)
                    {
                        PlanetSimulator.UpdatePlanetPosition(planet, planets, 0.2);
                    }*/

                    // Update the positions of the planet models in the 3D viewport
                    this.Dispatcher.Invoke(() =>
                    {
                        foreach (ModelVisual3D model in viewport3D.Children)
                        {
                            if (model.Content is GeometryModel3D geometry)
                            {
                                Debug.WriteLine($"MODEL CONTENT??????=============.");
                                foreach (Planet planet in planets)
                                {
                                    if (geometry.Transform is TranslateTransform3D transform && transform.OffsetX == planet.Position.X && transform.OffsetY == planet.Position.Y && transform.OffsetZ == planet.Position.Z)
                                    {
                                        Debug.WriteLine($"UPDATEING {planet.Name} =============.");
                                        PlanetSimulator.UpdatePlanetPosition(planet, planets, 0.0000001);
                                        transform.OffsetX = planet.Position.X;
                                        transform.OffsetY = planet.Position.Y;
                                        transform.OffsetZ = planet.Position.Z;
                                    }
                                }
                            }
                        }
                    });

                    // Wait for the specified time interval before updating again
                    Thread.Sleep(TimeSpan.FromSeconds(0.0000001));
                }
            });
        }

    }
}
