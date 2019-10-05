using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public abstract class CelestialBody : ICloneable //misschien interface ipv abstract
    {
        public string Name { get; set; }

        public List<string> Neighbours { get; set; }

        public double Radius { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double VX { get; set; }

        public double VY { get; set; }

        public virtual void onCollision()
        {
            if (collision != null)
                collision.Collide(this);
        }

        public CollisionComponent collision { get; set; }

        public abstract object Clone();

        public string Colour { get; set; }

    }
}
