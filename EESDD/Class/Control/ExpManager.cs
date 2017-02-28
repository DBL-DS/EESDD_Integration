using EESDD.Class.Model;
using EESDD.Module.Encryption;
using System;
using System.Collections.Generic;

namespace EESDD.Class.Control
{
    class ExpManager
    {
        public ExpManager()
        {
            evaluator = new Evaluator();
        }

        private Evaluator evaluator;
        private List<Exp> exps;
        private Dictionary<Tuple<string, string>, List<Exp>> gameExps;

        // Generate the name of exp file
        public static string GetFileName(string name)
        {
            return Encryptor.GetMD5(name) + ".exp";
        }

        public List<Exp> Exps
        {
            get
            {
                return exps;
            }
        }

        public int ExpCount
        {
            get
            {
                return gameExps.Values.Count;
            }
        }

        public bool Loading { get; set; }
        public bool Saving { get; set; }

        public int GetExpCount()
        {
            return ExpCount;
        }

        public int GetExpCount(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, mode);
            if (!gameExps.ContainsKey(key))
                return 0;
            else
                return gameExps[key].Count;
        }

        public Exp GetLastExp(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, scene);
            if (gameExps.ContainsKey(key)
                && gameExps[key].Count != 0)
                return gameExps[key][gameExps[key].Count - 1];
            else
                return null;           
        }

        public Exp GetLastExp()
        {
            if (exps == null || exps.Count == 0)
                return null;
            else
                return exps[exps.Count - 1];
        }

        public List<Exp> GetExps(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, scene);
            if (gameExps.ContainsKey(key)
                && gameExps[key].Count != 0)
                return gameExps[key];
            else
                return null;
        }


        // Read and load exp file
        public bool Load(Regular regular)
        {
            Loading = true;
            exps = FileManager.GetExps(GetFileName(regular.Name));

            if (exps == null)
            {
                Loading = false;
                return false;
            }

            foreach (var item in exps)
                AddExpToDict(item);

            Loading = false;
            return true;
        }

        private void LoadWithoutResult(object regular)
        {
            Load(regular as Regular);
        }

        public void ThreadLoad(Regular regular)
        {
            ThreadManager.DefineThread(ThreadCluster.LoadExp, LoadWithoutResult);
            ThreadManager.StartThread(ThreadCluster.LoadExp, regular);
        }

        public bool Save(Regular regular)
        {
            if (exps == null)
                return false;

            Saving = true;
            FileManager.SaveExps(exps, GetFileName(regular.Name));
            regular.ExpFile = GetFileName(regular.Name);
            Saving = false;

            return true;
        }

        private void SaveWithoutResult(object regular)
        {
            Save(regular as Regular);
        }

        public void ThreadSave(Regular regular)
        {
            ThreadManager.DefineThread(ThreadCluster.SaveExp, LoadWithoutResult);
            ThreadManager.StartThread(ThreadCluster.SaveExp, regular);
        }

        public void AddExp(Exp exp, bool evaluateNow)
        {
            if (evaluateNow)
                Evaluate(exp);
            if (exps == null)
                exps = new List<Exp>();
            exps.Add(exp);
            AddExpToDict(exp);
        }

        private void AddExpToDict(Exp exp)
        {
            if (gameExps == null)
                gameExps =
                    new Dictionary<Tuple<string, string>, List<Exp>>();

            var key = new Tuple<string, string>(exp.Scene, exp.Mode);
            if (!gameExps.ContainsKey(key))
                gameExps.Add(key, new List<Exp>());
        
            gameExps[key].Add(exp);
        }

        public bool RemoveExp(Exp exp)
        {
            var key = new Tuple<string, string>(exp.Scene, exp.Mode);
            if (gameExps.ContainsKey(key) 
                && gameExps[key].Contains(exp)
                && exps.Contains(exp))
            {
                exps.Remove(exp);
                gameExps[key].Remove(exp);

                if (gameExps[key].Count == 0)
                    gameExps.Remove(key);

                return true;                  
            }
            return false;
        }

        public bool RemoveLastExp(string scene, string mode)
        {
            var key = new Tuple<string, string>(scene, mode);

            int count = gameExps[key].Count;
            if (count == 0)
                return false;
            
            return RemoveExp(gameExps[key][count - 1]);
        }

        public bool RemoveLastExp()
        {
            if (exps == null || exps.Count == 0)
                return false;
            else
                return RemoveExp(exps[exps.Count - 1]);
        }

        public bool SaveExps(string expFileName)
        {
            return FileManager.SaveExps(exps, expFileName);
        }

        private Exp Evaluate(Exp exp)
        {
            if (!exp.Evaluated)
                evaluator.Evaluate(exp);

            return exp;
        }

        public void EvaluateAll()
        {
            if (exps == null || exps.Count == 0)
                return ;

            foreach (var exp in exps)
            {
                if (exp.Evaluated)
                    continue;

                evaluator.Evaluate(exp);
            }          
        }

        private Exp ReEvaluate(Exp exp)
        {
            evaluator.Evaluate(exp);

            return exp;
        }

        public void ReEvaluateAll()
        {
            if (exps == null || exps.Count == 0)
                return ;

            foreach (var exp in exps)
            {
                evaluator.Evaluate(exp);
            }
        }
    }
}
