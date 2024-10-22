﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlatGalaxy.Model.BreathFirstSSearch
{
    public class Vertex
    {
        public Vertex(string name, List<string> vertices)
        {
            Name = name;
            Vertices = vertices;
        }

        public string Name { get; set; }

        public List<string> Vertices { get; set; }
    }
}
