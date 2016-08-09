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

        # region Path Setting
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
        # endregion

        public static List<Scene> GetScenes()
        {
            string path = GetPath("setting", "scene");
            return ReadJson<List<Scene>>(path);
        }

        public static List<Mode> GetModes()
        {
            string path = GetPath("setting", "mode");
            return ReadJson<List<Mode>>(path);
        }

        public static List<GameIndex> GetGameIndexes()
        {
            string path = GetPath("setting", "game");
            return ReadJson<List<GameIndex>>(path);
        }

        public static List<Exp> GetExps(string expName)
        {
            string path = GetPath("exp", "path") + expName;
            return ReadJson<List<Exp>>(path);
        }

        public static Dictionary<string, string> GetUDPSetting()
        {
            string path = GetPath("setting", "udp");
            return ReadJson<Dictionary<string, string>>(path);
        }

        public static Dictionary<string, int> GetOffset()
        {
            string path = GetPath("setting", "offset");
            return ReadJson<Dictionary<string, int>>(path);
        }

        public static Dictionary<string,
            Dictionary<string, string>> GetStyle()
        {
            string path = GetPath("setting", "style");
            return ReadJson<Dictionary<string,
                Dictionary<string, string>>>(path);
        }

        public static Dictionary<string,
            Dictionary<string, Dictionary<string, string>>> GetText()
        {
            string path = GetPath("setting", "text");
            return ReadJson<Dictionary<string,
                Dictionary<string, Dictionary<string, string>>>>(path);
        }

        public static T ReadJson<T>(string path)
        {
            T result;
            try
            {
                string jsonStr = File.ReadAllText(path, Encoding.UTF8);
                result = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #region Save
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
        # endregion
    }
}
