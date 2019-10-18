using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatGalaxy_TomP.Controllers
{
    public class ViewController
    {
        public MainView MainView { get; set; }
        private Keys Key { get; set; }

        public List<Rectangle> bounds {
            get { return MainView.Rectangles; }
            set { MainView.Rectangles = value; }
        }
        public bool IsQuadTree
        {
            get { return MainView.IsQuadTree; }
            set { MainView.IsQuadTree = value; }
        }

        public ViewController(Dictionary<string, Keys> KeyBindings)
        {
            MainView = new MainView(KeyBindings);
            var bounds = Screen.GetBounds(new Point(100, 100));
        }

        public void runView()
        {
            Application.Run(MainView);
        }

        public bool isWebFile()
        {
            return MainView.isWebFile;
        }

        public string hasFile()
        {
            if (MainView.File != null && MainView.File.Length > 0) return MainView.File;
            return null;
        }

        public void resetFile()
        {
            MainView.File = null;
        }

        public void setSpeedText(string text)
        {
            MainView.setSpeedText(text);
        }

        public void drawFrame(List<CelestialBody> bodies)
        {
            MainView.setBodies(bodies);
            MainView.Refresh();
        }

        public bool hasKeyPressed()
        {
            return MainView.keyPressed != Keys.None ? true : false;
        }

        public string getKeyPressed(Dictionary<string, Keys> keyBindings) //TODO: actually use a pattern for this
        {
            Keys returnKey = MainView.keyPressed;
            MainView.keyPressed = Keys.None;
            string chosenKey = keyBindings.SingleOrDefault(b => b.Value == returnKey).Key;
            if(chosenKey != null)
                return chosenKey;
            return "none";
        }
    }
}
