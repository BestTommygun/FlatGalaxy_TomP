﻿using FlatGalaxy.Model;
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

        public List<ParserData> loadWebGalaxy(string adress) //TODO: should probably use a pattern for this
        {
            var parser = ParserFactory.returnParser(adress);
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(adress);
            return parser.Parse(stream);
        }

        public List<ParserData> loadLocalGalaxy(string file)
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

            IsPaused = false;


            while (true)
            {
                var file = ViewController.hasFile();

                if (file != null && file.Length > 0)
                {
                    List<ParserData> parserData;
                    if (ViewController.isWebFile())
                        parserData = loadLocalGalaxy(file);
                    else
                        parserData = loadWebGalaxy(file);
                    GalaxyBuilder galaxyBuilder = new GalaxyBuilder(); //hoeft niet opnieuw
                    Map Galaxy = galaxyBuilder.buildGalaxy(parserData);

                    ModelController = new ModelController(Galaxy);
                    InputController = new InputController(); //hoeft niet opnieuw
                    _collisionDetection = new NaiveCollision(); //hoeft niet opnieuw

                    simulationParams.TotalTime = TimeSpan.Zero;
                    _containsGalaxy = true;
                    SimulationSpeed = 2;
                    file = null;

                    ViewController.resetFile();
                    ViewController.setSpeedText("X" + SimulationSpeed);
                }

                //simLoop
                while (_containsGalaxy && !ViewController.MainView.HasFile)
                {
                    newTick = DateTime.UtcNow;

                    //TODO: quadtree, memento maybe, collision detection (half), keybindings refactor
                    //web loading refactor, botsen gedragsregels
                    //unit testing is 15% van het punt, urgh

                    //ALGA
                    //kortste pad: eigenlijk ding, configureerbare punten in UI?
                    //Goedkoopste pad: implementatie dijkstra (pythagoras), datastructuur, 
                    //hot dijkstra tip: pak steeds gwn wortel(C^2) als weight!
                    //QuadTree: alles lmao


                    if (ViewController.hasKeyPressed()) inputControl(ViewController.getKeyPressed(KeyBindings));

                    simulationParams.SetDelta(newTick - oldTick, SimulationSpeed);
                    ModelController.runGameTick(simulationParams);
                    _collisionDetection.Collide(ModelController.CurMap.celestialBodies);
                    ViewController.drawFrame(ModelController.CurMap.celestialBodies);

                    //DEBUG TODO: REMOVE
                    ALGABfSearch bfSearch = new ALGABfSearch();
                    bfSearch.BreathfirstSearch(ModelController.CurMap.celestialBodies, "Kobol", "Monea");

                    if(DateTime.UtcNow - newTick < _minTickTime)
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
        public void inputControl(string key)
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