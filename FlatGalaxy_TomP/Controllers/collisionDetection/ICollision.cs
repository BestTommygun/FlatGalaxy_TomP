using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.collisionDetection
{
    public interface ICollision
    {
        List<CelestialBody> Collide(List<CelestialBody> celestialBodies);

        List<Rectangle> GetBounds();
    }
}
