using EESDD.Class.Model;
using EESDD.Module.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // 指定exp文件的文件名格式
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


        // 读取并加载exp文件
        public bool Load(string expFileName)
        {
            gameExps = 
                new Dictionary<Tuple<string, string>, List<Exp>>();
            exps = FileManager.GetExps(expFileName);

            if (exps == null)
                return false;

            foreach (var item in exps)
            {
                AddExpToDict(item);
            } 

            return true;          
        }

        public bool Save(string expFileName)
        {
            if (exps == null)
                return false;
            
            FileManager.SaveExps(exps, expFileName);

            return true;
        }

        public void AddExp(Exp exp)
        {
            Evaluate(exp);
            exps.Add(exp);
            AddExpToDict(exp);
        }

        private void AddExpToDict(Exp exp)
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
