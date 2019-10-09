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
        Task<List<CelestialBody>> Collide(List<CelestialBody> celestialBodies);
    }
}
