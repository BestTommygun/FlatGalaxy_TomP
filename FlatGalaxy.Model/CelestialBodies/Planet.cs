using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public class Planet : CelestialBody
    {
        public override List<CelestialBody> onCollision()
        {
            return base.onCollision();
        }

        public override object Clone()
        {
            return new Planet()
            {
                Name = this.Name,
                Colour = this.Colour,
                Radius = this.Radius,
                Neighbours = this.Neighbours,
                collision = this.collision,
                X = this.X,
                Y = this.Y,
                VX = this.VX,
                VY = this.VY
            };
        }
    }
}
