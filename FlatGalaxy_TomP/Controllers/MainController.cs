using FlatGalaxy.Model;
using FlatGalaxy.Model.BreathFirstSSearch;
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
        private Dictionary<string, Keys> KeyBindings { get; set; }
        private ViewController ViewController { get; set; }
        private ModelController ModelController { get; set; }
        private ParserFactory ParserFactory { get; set; }
        private ICollision _collisionDetection;
        private IPathingAlgorithm _pathing;

        private double SimulationSpeed { get; set; }
        private bool _containsGalaxy { get; set; }
        private TimeSpan _minTickTime { get; set; }
        private bool IsPaused { get; set; }
        private bool Algorithms = true;

        public MainController()
        {
            _containsGalaxy = false;
            _minTickTime = TimeSpan.FromMilliseconds(1000 / 50);

            ParserFactory = new ParserFactory();
            KeyBindings = new Dictionary<string, Keys>();

            //TODO: think about enum
            KeyBindings.Add("back", Keys.A);
            KeyBindings.Add("pause", Keys.Space);
            KeyBindings.Add("faster", Keys.Add);
            KeyBindings.Add("slower", Keys.Subtract);
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

            if (ViewController.isWebFile())
                return loadLocalGalaxy(file);
            else
                return loadWebGalaxy(file);
        }

        private List<ParserData> loadWebGalaxy(string adress)
        {
            IParser parser = ParserFactory.returnParser(adress);
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(adress);
            return parser.Parse(stream);
        }

        private List<ParserData> loadLocalGalaxy(string file)
        {
            IParser parser = ParserFactory.returnParser(file);
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            return parser.Parse(fs);
        }

        public async void gameLoop()
        {
            DateTime oldTick = DateTime.UtcNow;
            DateTime newTick = DateTime.UtcNow;
            SimulationParams simulationParams = new SimulationParams(new TimeSpan(), new TimeSpan());
            GalaxyBuilder galaxyBuilder = new GalaxyBuilder();
            _collisionDetection = new NaiveCollision();
            _pathing = new BFSearch();
            IsPaused = false;

            while (true)
            {
                var file = ViewController.hasFile();

                if (file != null && file.Length > 0)
                {
                    Map Galaxy = galaxyBuilder.buildGalaxy(loadLocalGalaxy(file));

                    ModelController = new ModelController(Galaxy);

                    simulationParams.TotalTime = TimeSpan.Zero;
                    _containsGalaxy = true;
                    SimulationSpeed = 2;
                    file = null;

                    ViewController.resetFile();
                    ViewController.setSpeedText("X" + SimulationSpeed);
                }

                //simulationLoop
                while (_containsGalaxy && ViewController.MainView.File == null)
                {
                    newTick = DateTime.UtcNow;

                    //TODO: check public and private everywhere
                    //TODO: keybindings refactor, blink gedrag werkend krijgen
                    //unit testing is 15% van het punt, urgh
                    //TODO: dat vage factory refactoring ding gebruik dat PLEASE voor http & local
                    //TODO: use queue for the blink stuff?

                    //TODO: ALGA
                    //kortste pad: configureerbare punten in UI?
                    //Goedkoopste pad: configureerbaar in UI?
                    //QuadTree: houd rekening met randgevallen, 
                    //TODO: quadtree: kijk eens naar de grote planeten, die colliden bijna nooit doordat maar 1 hok runt


                    if (ViewController.hasKeyPressed()) inputControl(ViewController.getKeyPressed(KeyBindings));

                    simulationParams.SetDelta(newTick - oldTick, SimulationSpeed);
                    ModelController.runGameTick(simulationParams);

                    //collision related code
                    ModelController.CurMap.celestialBodies =  _collisionDetection.Collide(ModelController.CurMap.celestialBodies);
                    ViewController.bounds = _collisionDetection.GetBounds();

                    if (Algorithms) //this should only be enabled for the algorithm assignment
                    {
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

                    ViewController.drawFrame(ModelController.CurMap.celestialBodies);

                    if (DateTime.UtcNow - newTick < _minTickTime)
                    {
                        await Task.Delay(_minTickTime);
                    }
                    oldTick = newTick;

                    while (IsPaused && ViewController.MainView.File == null) //pause loop
                    {
                        await Task.Delay(_minTickTime);
                        if (ViewController.hasKeyPressed()) inputControl(ViewController.getKeyPressed(KeyBindings));

                        oldTick = DateTime.UtcNow;
                    }
                }
                await Task.Delay(_minTickTime);
            }
        }

        private void checkCollisionType()
        {
            if (ViewController.IsQuadTree && _collisionDetection.GetType() != typeof(QuadTreeCollision))
                _collisionDetection = new QuadTreeCollision();
            if (!ViewController.IsQuadTree && _collisionDetection.GetType() != typeof(NaiveCollision))
                _collisionDetection = new NaiveCollision();
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
