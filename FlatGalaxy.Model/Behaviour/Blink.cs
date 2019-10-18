using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatGalaxy.Model;

namespace FlatGalaxy.Model
{
    public class Blink : CollisionComponent
    {
        private string originalColour;

        public override List<CelestialBody> Collide(CelestialBody celestialBody)
        {
            if (originalColour == null) originalColour = celestialBody.Colour;

            int i = 10 - _todos.Count;

            while(i > 0)
            {
                _todos.Enqueue("yellow");
                _todos.Enqueue(originalColour);
                i--;
            }

            return base.Collide(celestialBody);
        }

        public override void doTodo(CelestialBody celestialBody)
        {
           
            if(_todos.Count > 0)
                celestialBody.Colour = _todos.Dequeue();
        }
    }
}
