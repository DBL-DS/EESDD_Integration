using EESDD.Class.Model;
using EESDD.Module.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class ExpManger
    {
        public static ExpCluster GetExpCluster(string fileName)
        {
            List<Exp> exps = FileManger.GetExps(fileName);
            return new ExpCluster(exps);
        }

        public static bool SaveExp(ExpCluster cluster, string fileName)
        {
            return FileManger.SaveExps(cluster.Exps, GetFileName(fileName));
        }

        public static string GetFileName(string name)
        {
            return Encryptor.GetMD5(name) + ".exp";
        }
    }
}
