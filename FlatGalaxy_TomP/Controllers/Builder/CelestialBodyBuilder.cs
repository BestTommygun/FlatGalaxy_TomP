
using System;
using System.Collections.Generic;
using FlatGalaxy.Model;
using FlatGalaxy.Model.Behaviour;
using FlatGalaxy_TomP.Controllers.collisionDetection;
using FlatGalaxy_TomP_JohanW.Controllers.parsing;

namespace FlatGalaxy_TomP_JohanW.Controllers
{
    public class CelestialBodyBuilder
    {
        public CelestialBody BuildCelestialBody(ParserData parserData)
        {
            if (parserData.Type.ToLower() == "planet") return BuildPlanet(parserData);
            else if (parserData.Type.ToLower() == "asteroid") return BuildAstroid(parserData);
            return new Astroid();
        }

        private Planet BuildPlanet(ParserData parserData)
        {
            Planet returningPlanet = new Planet()
            {
                Name = parserData.Name,
                Radius = parserData.Radius,
                X = parserData.X,
                Y = parserData.Y,
                VX = parserData.VX,
                VY = parserData.VY,
                Colour = parserData.Colour,
                Neighbours = parserData.Neighbours,
                collision = returnCollisionComponent(parserData.OnCollision)
            };

            return returningPlanet;
        }

        private Astroid BuildAstroid(ParserData parserData)
        {
            Astroid returningAstroid = new Astroid()
            {
                Neighbours = new List<string>(),
                Radius = parserData.Radius,
                X = parserData.X,
                Y = parserData.Y,
                VX = parserData.VX,
                VY = parserData.VY,
                Colour = parserData.Colour,
                collision = returnCollisionComponent(parserData.OnCollision)
        };

            return returningAstroid;
        }

        private CollisionComponent returnCollisionComponent(string collision)
        {
            switch (collision.ToLower())
            {
                case "blink":
                    return new Blink();
                case "bounce":
                    return new Bounce();
                case "dissapear":
                    return new Dissapear();
                case "explode":
                    return new Explode();
                case "grow":
                    return new Grow();
                default:
                    return new Blink();
            }
        }
    }
}
