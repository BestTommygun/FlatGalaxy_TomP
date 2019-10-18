using FlatGalaxy.Model;
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
        public Keys keyPressed { get; set; }
        public SimulationView simulationView { get; set; }
        public List<Rectangle> Rectangles
        {
            get { return simulationView.Rectangles; }
            set { simulationView.Rectangles = value; }
        }

        private Dictionary<string, Keys> Keybinds;
        private KeyBindingsConfig keyBindingsConfig;

        public MainView(Dictionary<string, Keys> keybinds)
        {
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(MainView_KeyDown);
            Keybinds = keybinds;
            IsQuadTree = false;
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

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            keyPressed = e.KeyData;
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keyBindingsConfig = new KeyBindingsConfig(Keybinds);
            keyBindingsConfig.Show();
        }

        private void webToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebFileLoader webFileLoader = new WebFileLoader();
            webFileLoader.ShowDialog();
            File = webFileLoader.webAdress;
            isWebFile = false;
        }

        private void enableQuadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsQuadTree = !IsQuadTree;
        }
    }
}
