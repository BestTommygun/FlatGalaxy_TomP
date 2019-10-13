using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FlatGalaxy_TomP_JohanW.Controllers.parsing
{
    public class XMLParser : IParser
    {
        public List<ParserData> Parse(Stream file)
        {         
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            List<ParserData> returningData = new List<ParserData>();

            using (XmlReader reader = XmlReader.Create(file, settings))
            {
                while (reader.Read() && reader.Name.ToLower() != "galaxy") { } //put here so the code ignores anything that isnt xml
                while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.MoveToContent(); //Make reader ignore whitespaces and endlines
                    ParserData parserData = new ParserData();
                    parserData.Neighbours = new List<string>();
                    parserData.Type = reader.Name;

                    while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.MoveToContent(); //Make reader ignore whitespaces and endlines

                        switch (reader.Name.ToLower())
                        {
                            case "name":
                                parserData.Name = reader.ReadElementContentAsString();
                                break;
                            case "position":
                                reader.Read();
                                reader.MoveToContent();
                                while(reader.NodeType != XmlNodeType.EndElement)
                                {
                                    switch (reader.Name)
                                    {
                                        case "x":
                                            parserData.X = reader.ReadElementContentAsDouble();
                                            break;
                                        case "y":
                                            parserData.Y = reader.ReadElementContentAsDouble();
                                            break;
                                        case "radius":
                                            parserData.Radius = reader.ReadElementContentAsInt();
                                            break;
                                        default:
                                            break;
                                    }
                                    reader.MoveToContent();
                                }
                                reader.ReadEndElement();
                                break;
                            case "speed":
                                reader.Read();
                                reader.MoveToContent();
                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    switch (reader.Name)
                                    {
                                        case "x":
                                            parserData.VX = reader.ReadElementContentAsDouble();
                                            break;
                                        case "y":
                                            parserData.VY = reader.ReadElementContentAsDouble();
                                            break;
                                        default:
                                            break;
                                    }
                                    reader.MoveToContent();
                                }
                                reader.ReadEndElement();
                                break;
                            case "neighbours":
                                reader.Read();
                                reader.MoveToContent();
                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    switch (reader.Name)
                                    {
                                        case "planet":
                                            parserData.Neighbours.Add(reader.ReadElementContentAsString());
                                            break;
                                        default:
                                            break;
                                    }
                                    reader.MoveToContent();
                                }
                                reader.ReadEndElement();
                                break;
                            case "color":
                                parserData.Colour = reader.ReadElementContentAsString();
                                break;
                            case "oncollision":
                                parserData.OnCollision = reader.ReadElementContentAsString();
                                break;
                            default:
                                break;
                        }
                    }
                    returningData.Add(parserData);
                }
            }
            return returningData;
        }
    }
}