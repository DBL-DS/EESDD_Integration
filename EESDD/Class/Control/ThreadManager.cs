using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    static class ThreadManager
    {
        static ThreadManager()
        {
            threads = new Dictionary<string, Thread>();
        }

        private static Dictionary<string, Thread> threads;

        public static Thread CreateThread(string name, ThreadStart func)
        {
            Thread thread = new Thread(func);
            thread.Name = name;
            threads.Add(name, thread);

            return thread;
        }

        public static bool StartThread(string threadName)
        {
            if (!threads.ContainsKey(threadName))
                return false;

            return StartThread(threads[threadName]);
        }

        public static bool StartThread(Thread thread)
        {
            thread.Start();
            return true;
        }

        public static bool StartThread(string threadName, object para)
        {
            if (!threads.ContainsKey(threadName))
                return false;

            return StartThread(threads[threadName], para);
        }

        public static bool StartThread(Thread thread, object para)
        {
            thread.Start(para);
            return true;
        }
    }
}
