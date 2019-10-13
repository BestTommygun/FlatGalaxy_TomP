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
        private ViewController ViewController { get; set; } //TODO: check public and private everywhere
        private ModelController ModelController { get; set; }
        private InputController InputController { get; set; }
        private ParserFactory ParserFactory { get; set; }
        private ICollision _collisionDetection;

        private double SimulationSpeed { get; set; }
        private bool _containsGalaxy { get; set; }
        private TimeSpan _minTickTime { get; set; }
        private bool IsPaused { get; set; }
        private bool Algorithms = true;

        public MainController()
        {
            _containsGalaxy = false;
            ParserFactory = new ParserFactory();
            _minTickTime = TimeSpan.FromMilliseconds(1000 / 50);
            KeyBindings = new Dictionary<string, Keys>();

            //TODO: think about enum
            KeyBindings.Add("back", Keys.A);
            KeyBindings.Add("pause", Keys.Space);
            KeyBindings.Add("faster", Keys.Add);
            KeyBindings.Add("slower", Keys.Subtract);
            ViewController = new ViewController(KeyBindings);

            gameLoop();
            ViewController.runView();
        }

        private List<ParserData> loadGalaxy(string file)
        {
            if (ViewController.isWebFile())
                return loadLocalGalaxy(file);
            else
                return loadWebGalaxy(file);
        }

        private List<ParserData> loadWebGalaxy(string adress) //TODO: should probably use a pattern for this
        {
            var parser = ParserFactory.returnParser(adress);
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(adress);
            return parser.Parse(stream);
        }

        private List<ParserData> loadLocalGalaxy(string file)
        {
            var parser = ParserFactory.returnParser(file);
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            return parser.Parse(fs);
        }

        public async void gameLoop()
        {
            DateTime oldTick = DateTime.UtcNow;
            DateTime newTick = DateTime.UtcNow;
            SimulationParams simulationParams = new SimulationParams(new TimeSpan(), new TimeSpan());
            GalaxyBuilder galaxyBuilder = new GalaxyBuilder();

            InputController = new InputController();
            _collisionDetection = new NaiveCollision();
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
                while (_containsGalaxy && !ViewController.MainView.HasFile)
                {
                    newTick = DateTime.UtcNow;

                    //TODO: keybindings refactor, blink gedrag werkend krijgen
                    //unit testing is 15% van het punt, urgh
                    //TODO: dat vage factory refactoring ding gebruik dat PLEASE voor http & local

                    //ALGA
                    //kortste pad: eigenlijk ding, configureerbare punten in UI?
                    //Goedkoopste pad: implementatie dijkstra (pythagoras), datastructuur, 
                    //hot dijkstra tip: pak steeds gwn wortel(C^2) als weight!
                    //QuadTree: alles lmao


                    if (ViewController.hasKeyPressed()) inputControl(ViewController.getKeyPressed(KeyBindings));

                    simulationParams.SetDelta(newTick - oldTick, SimulationSpeed);
                    ModelController.runGameTick(simulationParams);
                    ModelController.CurMap.celestialBodies =  await _collisionDetection.Collide(ModelController.CurMap.celestialBodies);

                    if (Algorithms) //this should only be enabled for the algorithm assignment
                    {
                        //TODO: Make an UI element to choose planets
                        BFSearch bFSearch = new BFSearch();
                        ModelController.CurMap.celestialBodies = bFSearch
                            .BreathFirstSearch(
                                ModelController.CurMap.celestialBodies,
                                ModelController.CurMap.celestialBodies[0],
                                ModelController.CurMap.celestialBodies[1]
                            );
                    }

                    ViewController.drawFrame(ModelController.CurMap.celestialBodies);

                    if (DateTime.UtcNow - newTick < _minTickTime)
                    {
                        await Task.Delay(_minTickTime);
                    }
                    oldTick = newTick;

                    while (IsPaused && !ViewController.MainView.HasFile) //pause loop
                    {
                        await Task.Delay(_minTickTime);
                        if (ViewController.hasKeyPressed()) inputControl(ViewController.getKeyPressed(KeyBindings));

                        oldTick = DateTime.UtcNow;
                    }
                }
                await Task.Delay(_minTickTime);
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
                default:
                    break;
            }
        }
    }
}
