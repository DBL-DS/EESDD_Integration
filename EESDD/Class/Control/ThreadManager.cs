using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    public enum ThreadCluster
    {
        PlayerRefresh,
        UDPTest,
        LoadExp,
        SaveExp
    }

    /*
     * ThreadManager manages all new threads.
     */
    static class ThreadManager
    {
        static ThreadManager()
        {
            threads = new Dictionary<ThreadCluster, Thread>();
        }

        private static Dictionary<ThreadCluster, Thread> threads;

        public static Thread DefineThread(ThreadCluster name,
            ThreadStart func)
        {
            Thread thread = new Thread(func);
            thread.Name = name.ToString();

            if (threads.ContainsKey(name))
                threads[name].Abort();

            threads[name] = thread;

            return thread;
        }
        public static Thread DefineThread(ThreadCluster name,
            ParameterizedThreadStart func)
        {
            Thread thread = new Thread(func);
            thread.Name = name.ToString();

            if (threads.ContainsKey(name))
                threads[name].Abort();

            threads[name] = thread;

            return thread;
        }

        public static bool StartThread(ThreadCluster name)
        {
            if (!threads.ContainsKey(name))
                return false;

            threads[name].Start();

            return true; ;
        }

        public static bool StartThread(ThreadCluster name, object para)
        {
            if (!threads.ContainsKey(name))
                return false;

            threads[name].Start(para);

            return true;
        }

        public static bool IsBusy()
        {
            foreach (var thread in threads.Values)
                if (thread.IsAlive && thread.Name != ThreadCluster.UDPTest.ToString())
                    return true;
            return false;
        }

        public static bool IsBusy(ThreadCluster name)
        {
            return threads.ContainsKey(name) && threads[name].IsAlive;
        }

        public static void KillAll()
        {
            foreach (var thread in threads.Values)
                thread.Abort();
        }

        public static void Hit() { }
    }
}
