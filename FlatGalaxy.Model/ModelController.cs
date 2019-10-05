using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatGalaxy.Model
{
    public class ModelController
    {
        private Dictionary<DateTime, Map> TimeStamps { get; set; }
        public Map CurMap { get; set; }

        public ModelController(Map curMap)
        {
            this.CurMap = curMap;
            TimeStamps = new Dictionary<DateTime, Map>();
            DateTime time = new DateTime();
            time = DateTime.UtcNow;
            TimeStamps.Add(time, (Map)curMap.Clone());
        }

        public void mapBackFiveSeconds() //backFive geen params?
        {
            var timestamp = TimeStamps.LastOrDefault().Key - TimeSpan.FromSeconds(5);

            if (timestamp > TimeStamps.FirstOrDefault().Key)
            {
                TimeStamps = TimeStamps.Where(k => k.Key <= timestamp).ToList().ToDictionary(k => k.Key, v => v.Value);
                CurMap = TimeStamps.LastOrDefault().Value;
                //time stuff
            }
        }

        public void newTimeStamp(DateTime time)//TODO: voor volle punt: denk aan efficiente maanier om memory leaks tegen te gaan
        {
            Console.WriteLine("saved {0} items", CurMap.celestialBodies.Count);
            TimeStamps.Add(time, (Map)CurMap.Clone()); //TODO: denk na over protype object dat opgeslagen word ipv map voor geheugen bewarens
        }

        public void runGameTick(SimulationParams simulationParams)
        {
            foreach (CelestialBody celestialBody in CurMap.celestialBodies)
            {
                //check for collision here
                var hiMom = celestialBody.VX * simulationParams.DeltaTime.TotalSeconds;
                celestialBody.X += celestialBody.VX * simulationParams.DeltaTime.TotalSeconds;
                celestialBody.Y += celestialBody.VY * simulationParams.DeltaTime.TotalSeconds;

                if (celestialBody.X > 800 - celestialBody.Radius)
                {
                    celestialBody.VX = -celestialBody.VX;
                    celestialBody.X = 800 - celestialBody.Radius;
                }else if (celestialBody.X < 0 + celestialBody.Radius)
                {
                    celestialBody.VX = -celestialBody.VX;
                    celestialBody.X = celestialBody.Radius;
                }
                if (celestialBody.Y > 600 - celestialBody.Radius)
                {
                    celestialBody.VY = -celestialBody.VY;
                    celestialBody.Y = 600 - celestialBody.Radius;
                }
                else if (celestialBody.Y < 0 + celestialBody.Radius)
                {
                    celestialBody.VY = -celestialBody.VY;
                    celestialBody.Y = celestialBody.Radius;
                }
            }
            if (simulationParams.TotalTime > TimeSpan.FromMilliseconds(100))
            {
                Console.WriteLine("adding new timestamp at {0}", DateTime.UtcNow);
                newTimeStamp(TimeStamps.LastOrDefault().Key + simulationParams.TotalTime);
                simulationParams.TotalTime = TimeSpan.Zero;
            }
        }
    }
}
