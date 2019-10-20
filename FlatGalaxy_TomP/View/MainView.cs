using FlatGalaxy.Model;
using FlatGalaxy_TomP.Controllers;
using FlatGalaxy_TomP.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatGalaxy_TomP
{
    public partial class MainView : Form
    {
        public string File { get; set; }
        public bool isWebFile { get; set; }
        public bool IsQuadTree { get; set; }
        public SimulationView simulationView { get; set; }
        private ViewController _viewController;
        public List<Rectangle> Rectangles
        {
            get { return simulationView.Rectangles; }
            set { simulationView.Rectangles = value; }
        }

        private KeyBindingsConfig keyBindingsConfig;

        public delegate void KeyPressedHandler(Keys keys);
        public event KeyPressedHandler KeyPressed;

        public MainView(ViewController controller)
        {
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += (object sender, KeyEventArgs args) => KeyPressed(args.KeyData);
            IsQuadTree = false;
            _viewController = controller;
        }

        public void setBodies(List<CelestialBody> celestialbodies)
        {
            simulationView.CelestialBodies = celestialbodies;
        }

        public void setSpeedText(string text)
        {
            simulationView.setSpeedText(text);
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = ".xml|.csv";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";
            openFileDialog1.Title = "Open a galaxy File...";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File = openFileDialog1.FileName;
                isWebFile = false;
            }
        }

        private void simulationView1_Load(object sender, EventArgs e)
        {
            simulationView = (SimulationView)sender;
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keyBindingsConfig = new KeyBindingsConfig(_viewController.KeyBindings);
            keyBindingsConfig.Show();
        }

        private void webToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebFileLoader webFileLoader = new WebFileLoader();
            webFileLoader.ShowDialog();
            File = webFileLoader.webAdress;
            isWebFile = true;
        }

        private void enableQuadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsQuadTree = !IsQuadTree;
        }
    }
}
