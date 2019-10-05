using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public abstract class CollisionComponent
    {
        public CollisionComponent nextCollision;

        public virtual void Collide(CelestialBody celestialBody)
        {
            if(nextCollision != null)
                nextCollision.Collide(celestialBody);
        }
    }
}
