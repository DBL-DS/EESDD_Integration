using EESDD.Class.Model;
using System;
using System.Collections.Generic;

namespace EESDD.Class.Control
{
    class GameManager
    {
        public GameManager()
        {

        }

        private Dictionary<Tuple<string, string>, GameIndex> indexDict;     // key - scene, mode;   value - GameIndex
        private Dictionary<GameIndex, Game> gameDict;                       // key - GameIndex;     value - Game
        private Dictionary<string, Scene> sceneDict;                        // key - scene.Name;    value - Scene
        private Dictionary<string, Mode> modeDict;                          // key - mode.Name;     value - Mode
        private Dictionary<string, List<Game>> sceneGames;                  // key - scene.Name;    value - List of Game whoes Game.Scene.Name == key
        private Dictionary<string, List<Game>> modeGames;                   // key - mode.Name;     value - List of Game whoes Game.Mode.Name == key

        public List<Game> Games
        {
            get { return new List<Game>(gameDict.Values); }
        }

        public List<GameIndex> GameIndexes
        {
            get { return new List<GameIndex>(indexDict.Values); }
        }

        public GameIndex GetGameIndex(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, mode);
            if (indexDict.ContainsKey(key))
                return indexDict[key];
            else
                return null;
        }

        public Game GetGame(string scene, string mode)
        {
            var key = GetGameIndex(scene, mode);
            if (key != null && gameDict.ContainsKey(key))
                return gameDict[GetGameIndex(scene, mode)];
            else
                return null;
        }

        public List<Scene> Scenes
        {
            get { return new List<Scene>(sceneDict.Values); }
            set
            {
                sceneDict = new Dictionary<string, Scene>();
                indexDict = null;
                gameDict = null;
                foreach (var item in value)
                {
                    AddScene(item);
                }
            }
        }

        public List<Mode> Modes
        {
            get { return new List<Mode>(modeDict.Values); }
            set
            {
                modeDict = new Dictionary<string, Mode>();
                indexDict = null;
                gameDict = null;
                foreach (var item in value)
                {
                    AddMode(item);
                }
            }
        }

        public int GameCount
        {
            get { return gameDict == null ? 0 : gameDict.Values.Count; }
        }

        public int SceneCount
        {
            get { return  sceneDict == null ? 0 : sceneDict.Values.Count; }
        }

        public int ModeCount
        {
            get { return  modeDict == null ? 0 : modeDict.Values.Count; }
        }

        public List<Game> GamesWithScene(string scene)
        {
            if (sceneGames.ContainsKey(scene))
                return sceneGames[scene];
            else
                return null;
        }

        public List<Game> GamesWithMode(string mode)
        {
            if (modeGames.ContainsKey(mode))
                return modeGames[mode];
            else
                return null;
        }

        public bool AddScene(Scene scene)
        {
            if (sceneDict == null)
                sceneDict = new Dictionary<string, Scene>();
            else if (sceneDict.ContainsKey(scene.Name))
                return false;

            sceneDict[scene.Name] = scene;
            return true;
        }

        public bool AddMode(Mode mode)
        {
            if (modeDict == null)
                modeDict = new Dictionary<string, Mode>();
            else if (modeDict.ContainsKey(mode.Name))
                return false;

            modeDict[mode.Name] = mode;
            return true;
        }

        public bool AddGame(GameIndex index)
        {
            if (indexDict == null || gameDict == null || sceneGames == null || modeGames == null)
            {
                indexDict = new Dictionary<Tuple<string, string>, GameIndex>();
                gameDict = new Dictionary<GameIndex, Game>();
                sceneGames = new Dictionary<string, List<Game>>();
                modeGames = new Dictionary<string, List<Game>>();
            }
            else if (indexDict.ContainsKey(new Tuple<string, string>(index.Scene, index.Mode)))
                return false;

            if (sceneDict != null && modeDict != null 
                && sceneDict.ContainsKey(index.Scene) 
                && modeDict.ContainsKey(index.Mode))
            {
                Game game = new Game(sceneDict[index.Scene], modeDict[index.Mode]);

                indexDict[new Tuple<string, string>(index.Scene, index.Mode)] = index;
                gameDict[index] = game;

                if (!sceneGames.ContainsKey(index.Scene) || sceneGames[index.Scene] == null)
                    sceneGames[index.Scene] = new List<Game>();
                sceneGames[index.Scene].Add(game);

                if (!modeGames.ContainsKey(index.Mode) || modeGames[index.Mode] == null)
                    modeGames[index.Mode] = new List<Game>();
                modeGames[index.Mode].Add(game);

                return true;
            }

            return false;
        }

        public bool AddGame(string scene, string mode)
        {
            return AddGame(new GameIndex(scene, mode));
        }

        public bool AddGame(Scene scene, Mode mode)
        {
            return AddGame(scene.Name, mode.Name);
        }

        public bool RemoveGame(string scene, string mode)
        {
            var tupleKey = new Tuple<string, string>(scene, mode);
            if (indexDict.ContainsKey(tupleKey))
            {
                Game gameToRemove = gameDict[indexDict[tupleKey]];

                gameDict.Remove(indexDict[tupleKey]);
                indexDict.Remove(tupleKey);

                if (sceneGames.ContainsKey(scene))
                {
                    sceneGames[scene].Remove(gameToRemove);
                    if (sceneGames[scene].Count == 0)
                        sceneGames.Remove(scene);
                }

                if (modeGames.ContainsKey(mode))
                {
                    modeGames[mode].Remove(gameToRemove);
                    if (modeGames[mode].Count == 0)
                        modeGames.Remove(mode);
                }

                return true;
            }

            return false;
        }

        public bool RemoveGame(GameIndex index)
        {
            return RemoveGame(index.Scene, index.Mode);
        }

        public bool RemoveGame(Game game)
        {
            return RemoveGame(game.Scene.Name, game.Mode.Name);
        }

        public bool RemoveScene(string scene)
        {
            if (sceneDict.ContainsKey(scene))
            {
                sceneDict.Remove(scene);
                List<Game> gamesToRemove = sceneGames[scene];
                sceneGames.Remove(scene);
                foreach (var item in gamesToRemove)
                {
                    RemoveGame(item);
                }

                return true;
            }

            return false;
        }

        public bool RemoveScene(Scene scene)
        {
            return RemoveScene(scene.Name);
        }

        public bool RemoveMode(string mode)
        {
            if (modeDict.ContainsKey(mode))
            {
                modeDict.Remove(mode);
                List<Game> gamesToRemove = modeGames[mode];
                modeGames.Remove(mode);
                foreach (var item in gamesToRemove)
                {
                    RemoveGame(item);
                }

                return true;
            }

            return false;
        }

        public bool RemoveMode(Mode mode)
        {
            return RemoveMode(mode.Name);
        }

        private int LoadScenes()
        {
            List<Scene> list = FileManager.GetScenes();
            Scenes = list;

            if (list == null)
                return -1;
            else
                return list.Count;
        }

        private int LoadModes()
        {
            List<Mode> list = FileManager.GetModes();
            Modes = list;

            if (list == null)
                return -1;
            else
                return list.Count;
        }

        private int LoadGames()
        {
            List<GameIndex> list = FileManager.GetGameIndexes();
            foreach (var item in list)
            {
                AddGame(item);
            }

            if (list == null)
                return -1;
            else
                return list.Count;
        }

        public bool Load()
        {
            return LoadScenes() != -1 && LoadModes() != -1 && LoadGames() != -1;
        }

        public bool Save()
        {
            return 
                FileManager.SaveScenes(Scenes) &&
                FileManager.SaveModes(Modes) &&
                FileManager.SaveGameIndexes(GameIndexes);
        }
    }
}
