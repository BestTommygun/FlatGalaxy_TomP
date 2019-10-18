using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP.Controllers.Algorithm
{
    public interface IPathingAlgorithm
    {
        List<CelestialBody> GetPath(List<CelestialBody> bodies, CelestialBody root, CelestialBody goal);
    }
}
