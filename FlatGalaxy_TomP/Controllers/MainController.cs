using FlatGalaxy.Model;
using FlatGalaxy_TomP.Controllers.Algorithm;
using FlatGalaxy_TomP.Controllers.collisionDetection;
using FlatGalaxy_TomP_JohanW.Controllers;
using FlatGalaxy_TomP_JohanW.Controllers.parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatGalaxy_TomP.Controllers
{
    public class MainController
    {
        private Dictionary<string, Keys> KeyBindings { get; }
        private ViewController ViewController { get; set; }
        private ModelController ModelController { get; set; }
        private ParserFactory ParserFactory { get; set; }
        private ICollision _collisionDetection;
        private IPathingAlgorithm _pathing;

        private double SimulationSpeed { get; set; }
        private bool ContainsGalaxy { get; set; }
        private TimeSpan MinTickTime { get; set; }
        private bool IsPaused { get; set; }
        private bool debugMode { get; set; }

        public MainController()
        {
            debugMode = false;
            ContainsGalaxy = false;
            MinTickTime = TimeSpan.FromMilliseconds(1000 / 50);

            ParserFactory = new ParserFactory();
            KeyBindings = new Dictionary<string, Keys>();

            //TODO: think about enum
            KeyBindings.Add("back", Keys.A);
            KeyBindings.Add("pause", Keys.Space);
            KeyBindings.Add("faster", Keys.Add);
            KeyBindings.Add("slower", Keys.Subtract);

            //keys for the algorithm assignment
            KeyBindings.Add("toggleDebugMode", Keys.T);
            KeyBindings.Add("add1", Keys.D1);
            KeyBindings.Add("add2", Keys.D2);
            KeyBindings.Add("add3", Keys.D3);
            KeyBindings.Add("remove1", Keys.NumPad1);
            KeyBindings.Add("remove2", Keys.NumPad2);
            KeyBindings.Add("remove3", Keys.NumPad3);
            KeyBindings.Add("switchCollision", Keys.C);
            KeyBindings.Add("switchPathing", Keys.P);
            ViewController = new ViewController(KeyBindings);

            gameLoop();
            ViewController.runView();
        }

        private List<ParserData> loadGalaxy(string file)
        {
            file.ToLower();
            Stream stream;
            IParser parser = ParserFactory.returnParser(file);

            if (ViewController.isWebFile())
            {
                WebClient client = new WebClient();
                stream = client.OpenRead(file);
            }
            else
            {
                stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            }
            return parser.Parse(stream);
        }

        public async void gameLoop()
        {
            DateTime oldTick = DateTime.UtcNow;
            DateTime newTick = DateTime.UtcNow;
            SimulationParams simulationParams = new SimulationParams(new TimeSpan(), new TimeSpan());
            GalaxyBuilder galaxyBuilder = new GalaxyBuilder();
            _collisionDetection = new QuadTreeCollision();
            _pathing = new BFSearch();
            IsPaused = false;

            while (true)
            {
                var file = ViewController.hasFile();

                if (file?.Length > 0)
                {
                    Map Galaxy = galaxyBuilder.buildGalaxy(loadGalaxy(file));

                    ModelController = new ModelController(Galaxy);

                    simulationParams.TotalTime = TimeSpan.Zero;
                    ContainsGalaxy = true;
                    SimulationSpeed = 2;
                    file = null;

                    ViewController.resetFile();
                    ViewController.setSpeedText("X" + SimulationSpeed);
                }

                //simulationLoop
                while (ContainsGalaxy && ViewController.MainView.File == null)
                {
                    newTick = DateTime.UtcNow;

                    //TODO: check public and private everywhere
                    //TODO: keybindings refactor, blink gedrag werkend krijgen
                    //unit testing is 15% van het punt, urgh
                    //TODO: dat vage factory refactoring ding gebruik dat PLEASE voor http & local & keybindings & parsers & 

                    inputControl(ViewController.getKeyPressed());

                    simulationParams.SetDelta(newTick - oldTick, SimulationSpeed);
                    ModelController.runGameTick(simulationParams);

                    //collision related code
                    ModelController.CurMap.celestialBodies =  _collisionDetection.Collide(ModelController.CurMap.celestialBodies);

                    if (debugMode) //this should only be enabled for the algorithm assignment, but it is a cool showcase of the program
                    {
                        ViewController.bounds = _collisionDetection.GetBounds();

                        List<CelestialBody> sortedList = ModelController.CurMap.celestialBodies
                                                            .OrderByDescending(x => x.Radius).ToList()
                                                            .Where(n => n.Name != null).ToList();

                        ModelController.CurMap.celestialBodies = _pathing
                            .GetPath(
                                ModelController.CurMap.celestialBodies,
                                sortedList[0],
                                sortedList[1]
                            );

                    }
                    else
                    {
                        ViewController.bounds = null;
                        ModelController.CurMap.celestialBodies.All(cb => { cb.IsMarked = false; return true; });
                    }

                    ViewController.drawFrame(ModelController.CurMap.celestialBodies);

                    if (DateTime.UtcNow - newTick < MinTickTime)
                    {
                        await Task.Delay(MinTickTime);
                    }
                    oldTick = newTick;

                    while (IsPaused && ViewController.MainView.File == null) //pause loop
                    {
                        await Task.Delay(MinTickTime);
                        inputControl(ViewController.getKeyPressed());

                        oldTick = DateTime.UtcNow;
                    }
                }
                await Task.Delay(MinTickTime);
            }
        }

        private void inputControl(string key)
        {
            switch (key)
            {
                case "faster":
                    if(SimulationSpeed < 1000)
                        SimulationSpeed = SimulationSpeed * 2;
                    ViewController.setSpeedText("X" + SimulationSpeed);
                    break;
                case "slower":
                    if (SimulationSpeed > 0.1)
                        SimulationSpeed = SimulationSpeed * 0.5;
                    ViewController.setSpeedText("X" + SimulationSpeed);
                    break;
                case "pause":
                    IsPaused = !IsPaused;
                    break;
                case "back":
                    ModelController.mapBackFiveSeconds();
                    break;
                case "toggleDebugMode":
                    debugMode = !debugMode;
                    break;
                case "add1":
                    ModelController.newAstroids(1);
                    break;
                case "add2":
                    ModelController.newAstroids(2);
                    break;
                case "add3":
                    ModelController.newAstroids(3);
                    break;
                case "switchCollision":
                    if (_collisionDetection.GetType() == typeof(QuadTreeCollision))
                        _collisionDetection = new NaiveCollision();
                    else
                        _collisionDetection = new QuadTreeCollision();
                    break;
                case "switchPathing":
                    if (_pathing.GetType() == typeof(DijkstraSearch))
                        _pathing = new BFSearch();
                    else
                        _pathing = new DijkstraSearch();
                    break;
                case "remove1":
                    ModelController.removeAstroids(1);
                    break;
                case "remove2":
                    ModelController.removeAstroids(2);
                    break;
                case "remove3":
                    ModelController.removeAstroids(3);
                    break;
                default:
                    break;
            }
        }
    }
}
