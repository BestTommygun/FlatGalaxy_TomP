using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlatGalaxy.Model;

namespace FlatGalaxy_TomP
{
    public partial class SimulationView : UserControl
    {
        public List<CelestialBody> CelestialBodies { get; set; }
        public List<Rectangle> Rectangles { get; set; }

        public SimulationView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            SpeedText.Visible = false;
            SpeedText.BackColor = Color.Transparent;
            label1.Visible = false;
            label1.BackColor = Color.Transparent;
        }

        public void setSpeedText(string text)
        {
            label1.Visible = true;
            SpeedText.Visible = true;
            SpeedText.Text = text;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (CelestialBodies != null)
            {
                //draw lines
                var bodies = CelestialBodies.Where(b => !string.IsNullOrWhiteSpace(b.Name)).ToDictionary(b => b.Name);
                foreach (var body in bodies.Select(b => b.Value))
                {
                    foreach (string neighbour in body.Neighbours)
                    {
                        Pen pen = new Pen(Color.Cyan);
                        if (body.IsMarked && bodies[neighbour].IsMarked)
                            pen = new Pen(Color.Red);
                        e.Graphics.DrawLine(
                            pen,
                            (float)body.X,
                            (float)body.Y,
                            (float)bodies[neighbour].X,
                            (float)bodies[neighbour].Y
                            );
                    }
                }

                foreach (CelestialBody body in CelestialBodies)
                {
                    Color penColour = Color.FromName(body.Colour ?? "DarkGray");
                    if (body.IsMarked)
                        penColour = Color.Red;

                        e.Graphics.FillEllipse(
                        new SolidBrush(penColour),
                        new RectangleF(
                            (float)(body.X - body.Radius),
                            (float)(body.Y - body.Radius),
                            (float)body.Radius * 2,
                            (float)body.Radius * 2
                            )
                        );
                }

                if(Rectangles?.Count > 0)
                {
                    foreach (Rectangle rectangle in Rectangles)
                    {
                        e.Graphics.DrawRectangle(
                            new Pen(Color.Red),
                            rectangle
                            );
                    }
                }
            }
        }
    }
}
