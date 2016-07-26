using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class ExpCluster
    {
        public ExpCluster()
        {

        }

        public ExpCluster(List<Exp> exps)
        {
            Exps = exps;
        }

        private Dictionary<Tuple<string, string>, List<Exp>> gameExps;

        public Dictionary<Tuple<string, string>, List<Exp>> GameExps
        {
            get { return gameExps; }
            set { gameExps = value; }
        }

        public List<Exp> Exps
        {
            get
            {
                List<Exp> exps = new List<Exp>();
                foreach (var item in gameExps)
	            {
                    exps.AddRange(item.Value);
	            }
                return exps;
            }
            set
            {
                foreach (var item in value)
                {
                    AddExp(item);
                }
            }
        }

        public Dictionary<Tuple<string, string>, int> Count
        {
            get
            {
                Dictionary<Tuple<string, string>, int> count 
                    = new Dictionary<Tuple<string, string>, int>();
                foreach (var exps in gameExps){
                    count.Add(exps.Key, exps.Value.Count);
                }
                return count;
            }
        }

        public int GameCount(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, mode);
            if (gameExps.ContainsKey(key))
                return gameExps[key].Count;
            else
                return 0;
        }

        public int GameCount(GameIndex index)
        {
            return GameCount(index.Scene, index.Mode);
        }

        public int GameCount(Game game)
        {
            return GameCount(game.Scene.Name, game.Mode.Name);
        }

        public void AddExp(Exp exp)
        {
            var key = new Tuple<string, string>(exp.Scene, exp.Mode);
            if (!gameExps.ContainsKey(key))
                gameExps.Add(key, new List<Exp>());
         
            gameExps[key].Add(exp);
        }

        public bool RemoveExp(Exp exp)
        {
            var key = new Tuple<string, string>(exp.Scene, exp.Mode);
            if (gameExps.ContainsKey(key) 
                && gameExps[key].Contains(exp))
            {
                gameExps[key].Remove(exp);

                if (gameExps[key].Count == 0)
                    gameExps.Remove(key);

                return true;                  
            }
            return false;
        }

        public Exp GetLastGameExp(string scene, string mode)
        {
            var key = new Tuple<string, string>
                (scene, scene);
            if (gameExps.ContainsKey(key)
                && gameExps[key].Count != 0)
                return gameExps[key][gameExps[key].Count - 1];
            else
                return null;
        }

        public Exp GetLastGameExp(GameIndex index)
        {
            return GetLastGameExp(index.Scene, index.Mode);
        }

        public Exp GetLastGameExp(Game game)
        {
            return GetLastGameExp(game.Scene.Name, game.Mode.Name);
        }

        public List<Exp> GetGameExps(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, mode);
            if (gameExps.ContainsKey(key) && gameExps[key].Count != 0)
                return gameExps[key];
            else
                return null;
        }

        public List<Exp> GetGameExps(GameIndex index)
        {
            return GetGameExps(index.Scene, index.Mode);
        }

        public List<Exp> GetGameExps(Game game)
        {
            return GetGameExps(game.Scene.Name, game.Mode.Name);
        }

    }
}
