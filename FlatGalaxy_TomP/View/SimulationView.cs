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
                        e.Graphics.DrawLine(
                            new Pen(Color.Cyan),
                            (float)body.X,
                            (float)body.Y,
                            (float)bodies[neighbour].X,
                            (float)bodies[neighbour].Y
                            );
                    }
                }

                //draw circles
                foreach (CelestialBody body in CelestialBodies)
                {
                    e.Graphics.FillEllipse(
                        new SolidBrush(
                            body.Colour == null || body.Colour.Equals("grey")
                                ? Color.Gray 
                                : Color.FromName(body.Colour)),
                        new RectangleF(
                            (float)(body.X - body.Radius),
                            (float)(body.Y - body.Radius),
                            (float)body.Radius*2,
                            (float)body.Radius*2
                            )
                        );
                }
                //TODO: draw collision cross cool thingies here
            }
        }
    }
}
