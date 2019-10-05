using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public interface ICollision
    {
        void Collide(List<CelestialBody> celestialBodies);
    }
}
