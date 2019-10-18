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

        public void removeAstroids(int amount)
        {
            Random random = new Random();

            var astroids = CurMap.celestialBodies.Where(n => n.Name == null).ToList();
            int index = amount;
            while(index > 0)
            {
                if(astroids.Count > 0)
                astroids.RemoveAt(random.Next(0, astroids.Count - 1));
                index--;
            }
            for (int i = 0; i < amount; i++)
            {
                CurMap.celestialBodies.Remove(CurMap.celestialBodies.Where(cb => !astroids.Contains(cb) && cb.Name == null).FirstOrDefault());
            }
        }

        public void newAstroids(int amount)
        {
            Random random = new Random();
            List<CelestialBody> newBodies = new List<CelestialBody>();
            while (amount > 0)
            {
                //random numbers for the new astroid
                double newVX = random.NextDouble() * 2 + 0.5; //0.5 - 2.5
                double newVY = random.NextDouble() * 2 + 0.5; //0.5 - 2.5
                double newR = random.NextDouble() * 2 + 1;    //1 - 3
                double newX = random.NextDouble() * 800;      // 0 - 800
                double newY = random.NextDouble() * 600;      // 0 - 600
                Astroid newAstroid = new Astroid()
                {
                    Neighbours = new List<string>(),
                    Radius = newR,
                    X = newX,
                    Y = newY,
                    VX = newVX,
                    VY = newVY,
                    Colour = "black",
                    collision = new Behaviour.Explode()
                };
                newBodies.Add(newAstroid);
                amount--;
            }
            CurMap.celestialBodies.AddRange(newBodies);
        }

        public void mapBackFiveSeconds()
        {
            var timestamp = TimeStamps.LastOrDefault().Key - TimeSpan.FromSeconds(5);

            if (timestamp > TimeStamps.FirstOrDefault().Key)
            {
                TimeStamps = TimeStamps.Where(k => k.Key <= timestamp).ToList().ToDictionary(k => k.Key, v => v.Value);
                CurMap = TimeStamps.LastOrDefault().Value;
            }
        }

        public void newTimeStamp(DateTime time)
        {
            
            if(TimeStamps.Count > 10000) //20 minuten
            {
                //remove all elements except for the first that exceed 10000
                var list = TimeStamps.ToList();
                list.RemoveRange(1, list.Count - 10000); //we want to keep the first element so you can still go back
                TimeStamps = list.ToDictionary(k => k.Key, v => v.Value);
                Console.WriteLine("Edited map to contain {0} items...", TimeStamps.Count);
            }

            Console.WriteLine("saved {0} items", CurMap.celestialBodies.Count);
            TimeStamps.Add(time, (Map)CurMap.Clone());
        }

        public void runGameTick(SimulationParams simulationParams)
        {
            foreach (CelestialBody celestialBody in CurMap.celestialBodies)
            {
                celestialBody.X += celestialBody.VX * simulationParams.DeltaTime.TotalSeconds;
                celestialBody.Y += celestialBody.VY * simulationParams.DeltaTime.TotalSeconds;

                if (celestialBody.X > 800 - celestialBody.Radius)
                {
                    celestialBody.VX = -celestialBody.VX;
                    celestialBody.X = 800 - celestialBody.Radius;
                }
                else if (celestialBody.X < 0 + celestialBody.Radius)
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
