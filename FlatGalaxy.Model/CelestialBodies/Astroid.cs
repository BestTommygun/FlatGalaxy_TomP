using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public class Astroid : CelestialBody
    {
        public async override Task<List<CelestialBody>> onCollision()
        {
            return await base.onCollision();
        }

        public override object Clone()
        {
            return new Astroid()
            {
                Name = this.Name,
                Colour = this.Colour,
                Radius = this.Radius,
                collision = this.collision,
                X = this.X,
                Y = this.Y,
                VX = this.VX,
                VY = this.VY
            };
        }
    }
}
