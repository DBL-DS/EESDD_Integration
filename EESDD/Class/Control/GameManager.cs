using EESDD.Class.Model;
using System.Collections.Generic;

namespace EESDD.Class.Control
{
    class GameManager
    {
        public GameManager()
        {
            games = new List<Game>();
            scenes = new HashSet<Scene>();
            modes = new HashSet<Mode>();
        }

        private List<Game> games;
        private HashSet<Scene> scenes;
        private HashSet<Mode> modes;
        private Dictionary<Scene, List<Game>> sceneGames;
        private Dictionary<Mode, List<Game>> modeGames;

        public List<Game> Games
        {
            get { return games; }
        }

        public HashSet<Scene> Scenes
        {
            get { return scenes; }
        }

        public HashSet<Mode> Modes
        {
            get { return modes; }
        }

        public Dictionary<Scene, List<Game>> SceneGames
        {
            get { return sceneGames; }
        }

        public Dictionary<Mode, List<Game>> ModeGames
        {
            get { return modeGames; }
        }

        public void AddGame(Game game)
        {
            // Add game to game list
            games.Add(game);

            // Add scene
            scenes.Add(game.Scene);

            // Add mode
            modes.Add(game.Mode);

            // Add game to game list with scene index
            if (sceneGames.ContainsKey(game.Scene))
            {
                sceneGames[game.Scene].Add(game);
            }
            else
            {
                sceneGames.Add(game.Scene, new List<Game>());
                sceneGames[game.Scene].Add(game);
            }

            // Add game to game list with mode index
            if (modeGames.ContainsKey(game.Mode))
            {
                modeGames[game.Mode].Add(game);
            }
            else
            {
                modeGames.Add(game.Mode, new List<Game>());
                modeGames[game.Mode].Add(game);
            }
        }

        public void RemoveGame(Game game)
        {
            // Remove game from game list
            if (games.Contains(game))
            {
                games.Remove(game);          
            }

            if (sceneGames.ContainsKey(game.Scene))
            {
                // Remove game from game list with scene index
                if (sceneGames[game.Scene].Contains(game))
                    sceneGames[game.Scene].Remove(game);

                // Remove scene from scene list if there isn't a mode under this scene
                if (sceneGames[game.Scene].Count == 0)
                {
                    sceneGames.Remove(game.Scene);
                    if (scenes.Contains(game.Scene))
                    {
                        scenes.Remove(game.Scene);
                    }
                }
            }

            if (modeGames.ContainsKey(game.Mode))
            {
                // Remove game from game list with mode index
                if (modeGames[game.Mode].Contains(game))
                    modeGames[game.Mode].Remove(game);

                // Remove mode from mode list if there isn't a scene under this mode
                if (modeGames[game.Mode].Count == 0)
                {
                    modeGames.Remove(game.Mode);
                    if (modes.Contains(game.Mode))
                    {
                        modes.Remove(game.Mode);
                    }
                }
            }     
        }

    }
}
