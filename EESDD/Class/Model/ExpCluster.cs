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
            gameExps = new Dictionary<Game, List<Exp>>();
        }

        private Dictionary<Game, List<Exp>> gameExps;

        public Dictionary<Game, List<Exp>> GameExps
        {
            get { return gameExps; }
        }

        public Dictionary<Game, int> Count
        {
            get
            {
                Dictionary<Game, int> count = new Dictionary<Game,int>();
                foreach (var exps in gameExps){
                    count.Add(exps.Key, exps.Value.Count);
                }
                return count;
            }
        }

        public int GameCount(Game game)
        {
            if (gameExps.ContainsKey(game))
                return gameExps[game].Count;
            else
                return 0;
        }

        public void AddExp(Game game, Exp exp)
        {
            if (!gameExps.ContainsKey(game))
                gameExps.Add(game, new List<Exp>());
         
            gameExps[game].Add(exp);
        }

        public bool RemoveExp(Exp exp)
        {
            if (gameExps.ContainsKey(exp.Game) && gameExps[exp.Game].Contains(exp))
            {
                gameExps[exp.Game].Remove(exp);

                if (gameExps[exp.Game].Count == 0)
                    gameExps.Remove(exp.Game);

                return true;                  
            }
            return false;
        }

        public Exp GetLastGameExp(Game game)
        {
            if (gameExps.ContainsKey(game) && gameExps[game].Count != 0)
                return gameExps[game][gameExps[game].Count - 1];
            else
                return null;
        }

        public List<Exp> GetGameExps(Game game)
        {
            if (gameExps.ContainsKey(game) && gameExps[game].Count != 0)
                return gameExps[game];
            else
                return null;
        }

    }
}
