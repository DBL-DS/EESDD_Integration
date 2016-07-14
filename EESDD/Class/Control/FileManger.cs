using EESDD.Class.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    static class FileManger
    {
        static FileManger()
        {
            WorkPath = System.Environment.CurrentDirectory;
            LoadPath();
        }

        public static Dictionary<string, Dictionary<string, string>> Path;
        public static string WorkPath;

        // 加载 path.json
        private static bool LoadPath()
        {
            string path = FindFile("path.json");
            if (path != null)
            {
                string jsonStr = File.ReadAllText(path);
                Path = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonStr);

                return true;
            }

            return false;
        }

        // 返回工作目录（包括子目录）下，找到的第一个符合文件名要求文件的完整路径
        private static string FindFile(string fileName)
        {
            var paths = Directory.GetFiles(WorkPath, fileName, SearchOption.AllDirectories);
            if (paths.Length > 0)
                return paths[0];
            else
                return null;
        }

        public static string GetPath(string category, string key)
        {
            if (Path.ContainsKey(category))
                if (Path[category].ContainsKey(key))
                    return WorkPath + Path[category][key];

            return null;
        }

        public static List<Scene> GetScenes()
        {
            List<Scene> scenes;

            string scenePath = GetPath("setting", "scene");
            string jsonStr = File.ReadAllText(scenePath, Encoding.UTF8);
            scenes = JsonConvert.DeserializeObject<List<Scene>>(jsonStr);

            return scenes;
        }

        public static List<Mode> GetModes()
        {
            List<Mode> modes;

            string modePath = GetPath("setting", "mode");
            string jsonStr = File.ReadAllText(modePath, Encoding.UTF8);
            modes = JsonConvert.DeserializeObject<List<Mode>>(jsonStr);

            return modes;
        }

        public static List<GameIndex> GetGameIndexes()
        {
            List<GameIndex> games;

            string gamePath = GetPath("setting", "game");
            string jsonStr = File.ReadAllText(gamePath, Encoding.UTF8);
            games = JsonConvert.DeserializeObject<List<GameIndex>>(jsonStr);
            
            return games;
        }
    }
}
