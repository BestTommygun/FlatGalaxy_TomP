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
        public Dictionary<string, Keys> KeyBindings { get; }
        private Queue<Keys> KeysPressed;
        
        public List<Rectangle> bounds {
            get { return MainView.Rectangles; }
            set { MainView.Rectangles = value; }
        }
        public bool IsQuadTree
        {
            get { return MainView.IsQuadTree; }
            set { MainView.IsQuadTree = value; }
        }

        
        public ViewController(Dictionary<string, Keys> keyBindings)
        {
            KeyBindings = keyBindings;
            var bounds = Screen.GetBounds(new Point(100, 100));
            KeyBindings = keyBindings;
            KeysPressed = new Queue<Keys>();
            MainView = new MainView(this);
            MainView.KeyPressed += (Keys keys) => KeysPressed.Enqueue(keys);
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
            if (MainView.File?.Length > 0) return MainView.File;
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

        public string getKeyPressed()
        {
            Keys returnKey = KeysPressed.Count > 0 ? KeysPressed.Dequeue() : Keys.None;
            return KeyBindings.SingleOrDefault(b => b.Value == returnKey).Key;
        }
    }
}
