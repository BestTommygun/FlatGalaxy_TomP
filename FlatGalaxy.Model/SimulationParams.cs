using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatGalaxy.Model
{
    public class SimulationParams
    {
        public TimeSpan TotalTime { get; set; }

        public TimeSpan DeltaTime { get; set; }

        public SimulationParams (TimeSpan totalTime, TimeSpan deltaTime)
        {
            TotalTime = TotalTime;
            DeltaTime = deltaTime;
        }

        public void SetDelta(TimeSpan timeSpan, double speed) //speed is a value between 0.1 and 100
        {
            DeltaTime = TimeSpan.FromMilliseconds(timeSpan.Milliseconds * speed);
            TotalTime += timeSpan;
        }
    }
}
