using FlatGalaxy.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public abstract class CollisionComponent
    {
        public CollisionComponent nextCollision { get; set; }
        public Queue<string> _todos { get; set; }

        public CollisionComponent()
        {
            _todos = new Queue<string>();
        }

        public virtual void doTodo(CelestialBody celestialBody)
        {
            if (_todos.Count > 0)
                _todos.Dequeue();
        }


        public virtual List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            List<CelestialBody> returnList = new List<CelestialBody>();

            if (nextCollision != null)
                returnList = nextCollision.Collide(celestialBody);
            else
                returnList.Add(celestialBody);

            return returnList;
        }
    }
}
