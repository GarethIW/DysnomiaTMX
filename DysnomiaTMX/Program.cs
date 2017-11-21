using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DysnomiaTMX
{
    public class Device
    {
        public int X;
        public int Y;
        public int Type;
        public int ReqMotherboards;
        public int CurMotherboards;
        public string Name;
        public string DisplayName;
        public string TerminalText;
    }

    public class LayerInfo
    {
        public string DisplayName;
        public string TileSet;
        public int FirstGID;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mapNum = 1;

            string inFn = "maps\\01.txt";
            string outFn = "01.json";


            var layers = new LayerInfo[]
            {
                new LayerInfo(){DisplayName = "Floor", TileSet = "Floor", FirstGID=1},
                new LayerInfo(){DisplayName = "Floor Anim", TileSet = "Anim", FirstGID = 2500},
                new LayerInfo(){DisplayName = "Reflections (Floor)", TileSet = "Reflections", FirstGID = 3500},
                new LayerInfo(){DisplayName = "Decals", TileSet = "Decals", FirstGID=1000},
                new LayerInfo(){DisplayName = "Reflections (Decal)", TileSet = "DecalReflections", FirstGID = 3000},
                new LayerInfo(){DisplayName = "Pickups", TileSet = "Pickups", FirstGID = 4000},
                new LayerInfo(){DisplayName = "Shadows", TileSet = "Shadows", FirstGID = 1500},
                new LayerInfo(){DisplayName = "Walls", TileSet = "Walls", FirstGID = 500},
                new LayerInfo(){DisplayName = "Wall Anim", TileSet = "Anim", FirstGID = 2500},
                new LayerInfo(){DisplayName = "Reflections (Wall)", TileSet = "Reflections", FirstGID = 3500},
                new LayerInfo(){DisplayName = "Wall Decals", TileSet = "Decals", FirstGID = 1000},
                new LayerInfo(){DisplayName = "Reflections (Wall Decal)", TileSet = "DecalReflections", FirstGID = 3000},
                new LayerInfo(){DisplayName = "Overhead", TileSet = "Overhead", FirstGID = 2000},
            };

            using (var reader = File.OpenText(inFn))
            {
                var xs = Convert.ToInt32(reader.ReadLine());
                var ys = Convert.ToInt32(reader.ReadLine());

                var map = new int[13, xs, ys];

                for (var layer = 0; layer < 13; layer++)
                {
                    for (var y = 0; y < ys; y++)
                    {
                        var line = reader.ReadLine();
                        for (var x = 0; x < xs; x++)
                        {
                            map[layer, x, y] = Convert.ToInt32(line.Substring(x * 3, 3));
                        }
                    }
                }

                var lightLevel = Convert.ToInt32(reader.ReadLine());
                var numDevices = Convert.ToInt32(reader.ReadLine());

                var devices = new List<Device>();

                for (int i = 0; i < numDevices; i++)
                {
                    var line = reader.ReadLine();

                    string[] splitrow = line.Split(',');
                    Device d = new Device();

                    d.X = Convert.ToInt32(splitrow[0]);
                    d.Y = Convert.ToInt32(splitrow[1]);
                    d.Type = Convert.ToInt32(splitrow[2]);
                    d.DisplayName = splitrow[3];
                    d.Name = splitrow[4];
                    d.ReqMotherboards = Convert.ToInt32(splitrow[5]);
                    d.CurMotherboards = Convert.ToInt32(splitrow[6]);
                    d.TerminalText = splitrow[7].Replace("||", "\r\n").Replace("|+", ",");

                    devices.Add(d);
                }

                var tileSets = "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":1," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/floors.png\"," +
                               "      \"imageheight\":576," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Floor\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":90," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":500," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/walls.png\"," +
                               "      \"imageheight\":1728," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Walls\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":270," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":1000," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/decals.png\"," +
                               "      \"imageheight\":1344," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Decals\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":210," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":1500," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/shadows.png\"," +
                               "      \"imageheight\":1280," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Shadows\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":200," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":2000," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/overhead.png\"," +
                               "      \"imageheight\":640," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Overhead\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":100," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":2500," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/anims.png\"," +
                               "      \"imageheight\":256," +
                               "      \"imagewidth\":768," +
                               "      \"margin\":0," +
                               "      \"name\":\"Anim\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":48," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":3000," +
                               "      \"image\":\"tiles\\/" + mapNum + "\\/decalreflections.png\"," +
                               "      \"imageheight\":1024," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"DecalReflections\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":160," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":3500," +
                               "      \"image\":\"tiles\\/reflection.png\"," +
                               "      \"imageheight\":640," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Reflections\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":100," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }," +
                               "    {" +
                               "      \"columns\":10," +
                               "      \"firstgid\":4000," +
                               "      \"image\":\"tiles\\/pickups.png\"," +
                               "      \"imageheight\":640," +
                               "      \"imagewidth\":640," +
                               "      \"margin\":0," +
                               "      \"name\":\"Pickups\"," +
                               "      \"spacing\":0," +
                               "      \"tilecount\":100," +
                               "      \"tileheight\":64," +
                               "      \"tilewidth\":64" +
                               "    }";


                var tileLayers = "";
                for (var l = 0; l < 13; l++)
                {
                    var data = "";
                    for(var y=0;y<ys;y++)
                        for (var x = 0; x < xs; x++)
                            data += (map[l, x, y] + layers[l].FirstGID) + ",";

                    data = data.TrimEnd(',');

                    tileLayers+= "{" + 
                        "\"data\":["+data+"]," + 
                        "\"height\":"+ys+"," +
                        "\"name\":\""+ layers[l].DisplayName +"\"," +
                        "\"opacity\":1," +
                        "\"type\":\"tilelayer\"," +
                        "\"visible\":true," +
                        "\"width\":"+xs+"," +
                        "\"x\":0," +
                        "\"y\":0" +
                        "},";
                }

                tileLayers = tileLayers.TrimEnd(',');

                var outMap = "{" +
                             "  \"backgroundcolor\":\"#000000\"," +
                             "  \"height\":"+ ys +"," +
                             "  \"layers\":["+tileLayers+"]," +
                             "  \"nextobjectid\":"+(numDevices+1)+"," +
                             "  \"orientation\":\"orthogonal\"," +
                             "  \"properties\": {" + 
                             "    \"LightLevel\":"+lightLevel +
                             "  }," +
                             "  \"renderorder\":\"right-down\"," +
                             "  \"tileheight\":64," +
                             "  \"tilesets\":["+tileSets+"]," +
                             "  \"tilewidth\":64," +
                             "  \"version\":1," +
                             "  \"tiledversion\":\"1.0.3\"," +
                             "  \"width\":"+xs +
                             "}";

                File.WriteAllText(outFn,outMap);
            }
        }
    }
}
