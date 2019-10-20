using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public abstract class CelestialBody : ICloneable
    {
        public string Name { get; set; }

        public List<string> Neighbours { get; set; }

        public double Radius { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double VX { get; set; }

        public double VY { get; set; }

        public virtual List<CelestialBody> onCollision()
        {
            List<CelestialBody> returnList = new List<CelestialBody>();

            if (collision != null)
                returnList = collision.Collide(this);
            else
                returnList.Add(this);

            return returnList;
        }

        public CollisionComponent collision { get; set; }

        public bool ShouldDissapear { get; set; } = false;

        public abstract object Clone();

        public string Colour { get; set; }

        public bool IsMarked { get; set; }
    }
}
