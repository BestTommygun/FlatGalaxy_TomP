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

        public async virtual Task<List<CelestialBody>> Collide(CelestialBody celestialBody)
        {
            List<CelestialBody> returnList = new List<CelestialBody>();

            if (nextCollision != null)
                returnList = await nextCollision.Collide(celestialBody);
            else
                returnList.Add(celestialBody);

            return returnList;
        }
    }
}
