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

        private static Dictionary<string, Dictionary<string, string>> Path;
        private static string WorkPath;

        // 加载 path.json
        private static bool LoadPath()
        {
            string path = FindFile("path.json");
            if (path != null)
            {
                try
                {
                    string jsonStr = File.ReadAllText(path);
                    Path = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonStr);

                }
                catch (Exception)
                {
                    return false;
                }

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
            try
            {
                string jsonStr = File.ReadAllText(scenePath, Encoding.UTF8);
                scenes = JsonConvert.DeserializeObject<List<Scene>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }

            return scenes;
        }

        public static List<Mode> GetModes()
        {
            List<Mode> modes;

            string modePath = GetPath("setting", "mode");
            try
            {
                string jsonStr = File.ReadAllText(modePath, Encoding.UTF8);
                modes = JsonConvert.DeserializeObject<List<Mode>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }

            return modes;
        }

        public static List<GameIndex> GetGameIndexes()
        {
            List<GameIndex> games;

            string gamePath = GetPath("setting", "game");
            try
            {
                string jsonStr = File.ReadAllText(gamePath, Encoding.UTF8);
                games = JsonConvert.DeserializeObject<List<GameIndex>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }
            
            return games;
        }

        public static List<Exp> GetExps(string expName)
        {
            List<Exp> exps;

            string expPath = GetPath("exp", "path") + expName;
            try
            {
                string jsonStr = File.ReadAllText(expPath, Encoding.UTF8);
                exps = JsonConvert.DeserializeObject<List<Exp>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }

            return exps;
        }

        public static Dictionary<string, string> GetUDPSetting()
        {
            Dictionary<string, string> udp;

            string udpPath = GetPath("setting", "udp");
            try
            {
                string jsonStr = File.ReadAllText(udpPath, Encoding.UTF8);
                udp = JsonConvert.DeserializeObject
                    <Dictionary<string, string>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }

            return udp;
        }

        public static Dictionary<string, int> GetOffset()
        {
            Dictionary<string, int> offset;

            string udpPath = GetPath("setting", "offset");
            try
            {
                string jsonStr = File.ReadAllText(udpPath, Encoding.UTF8);
                offset = JsonConvert.DeserializeObject
                    <Dictionary<string, int>>(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }

            return offset;
        }

        public static bool SaveScenes(List<Scene> scenes)
        {
            string jsonStr = JsonConvert.SerializeObject(scenes);
            string scenePath = GetPath("setting", "scene");

            return WriteFile(scenePath, jsonStr);
        }

        public static bool SaveModes(List<Mode> modes)
        {
            string jsonStr = JsonConvert.SerializeObject(modes);
            string modePath = GetPath("setting", "mode");

            return WriteFile(modePath, jsonStr);
        }

        public static bool SaveGameIndexes(List<GameIndex> indexes)
        {
            string jsonStr = JsonConvert.SerializeObject(indexes);
            string gamePath = GetPath("setting", "game");

            return WriteFile(gamePath, jsonStr);
        }

        public static bool SaveExps(List<Exp> exps, string expName)
        {
            string jsonStr = JsonConvert.SerializeObject(exps);
            string expPath = GetPath("exp", "path") + expName;

            return WriteFile(expPath, jsonStr);
        }

        public static bool SaveUDPSetting(Dictionary<string, string> udp)
        {
            string jsonStr = JsonConvert.SerializeObject(udp);
            string udpPath = GetPath("setting", "udp");

            return WriteFile(udpPath, jsonStr);            
        }

        private static bool WriteFile(string path, string str)
        {
            try
            {
                File.WriteAllText(path, str);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
    }
}
