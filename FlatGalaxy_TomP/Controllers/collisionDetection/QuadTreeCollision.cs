using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public class QuadTreeCollision : ICollision
    {
        public async Task<List<CelestialBody>> Collide(List<CelestialBody> celestialBodies)
        {
            throw new NotImplementedException(); //TODO: quadtree collision
        }
    }
}
