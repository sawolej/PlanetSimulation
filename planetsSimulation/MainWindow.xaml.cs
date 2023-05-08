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
            meshBuilder.AddSphere(planet.Position, 0.5); // Change 0.5 to the desired planet size

            // Create the planet material (color, texture, etc.)
            DiffuseMaterial planetMaterial = new DiffuseMaterial(new SolidColorBrush(planet.Color));

            // Create and return the planet 3D model
            return new ModelVisual3D
            {
                Content = new GeometryModel3D(meshBuilder.ToMesh(), planetMaterial)
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
        }
    }
}
