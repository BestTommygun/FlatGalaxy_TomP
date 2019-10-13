using FlatGalaxy.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    class CSVParser : IParser
    {
        public List<ParserData> Parse(Stream file)
        {
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                bool fileIsValid = false;
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                List<ParserData> parserData = new List<ParserData>();
                string[] headerFields = parser.ReadFields(); // read header and validate
                if (headerFields.Length == 10
                    && headerFields[0].ToLower().Equals("name")
                    && headerFields[1].ToLower().Equals("type")
                    && headerFields[2].ToLower().Equals("x")
                    && headerFields[3].ToLower().Equals("y")
                    && headerFields[4].ToLower().Equals("vx")
                    && headerFields[5].ToLower().Equals("vy")
                    && headerFields[6].ToLower().Equals("neighbours")
                    && headerFields[7].ToLower().Equals("radius")
                    && headerFields[8].ToLower().Equals("color")
                    && headerFields[9].ToLower().Equals("oncollision"))
                {
                    fileIsValid = true;
                }
                else
                {
                    throw new FormatException();
                }

                    //process row
                while (!parser.EndOfData && fileIsValid)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length == 10)
                    {
                        List<string> neighbours = fields[6].Split(',').ToList();
                        parserData.Add(new ParserData()
                        {
                            Name = fields[0],
                            Type = fields[1],
                            X = Double.Parse(fields[2]),
                            Y = Double.Parse(fields[3]),
                            VX = Double.Parse(fields[4]),
                            VY = Double.Parse(fields[5]),
                            Neighbours = neighbours,
                            Radius = int.Parse(fields[7]),
                            Colour = fields[8],
                            OnCollision = fields[9]
                        });
                    }
                }
                return parserData;
            }
        }
    }
}
