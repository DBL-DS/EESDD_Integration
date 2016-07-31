using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class Player
    {
        public Player()
        {
            refreshThread = ThreadManager.DefineThread(
                ThreadCluster.PlayerRefresh, Refresh);
        }

        private Scene scene;
        private Mode mode;
        private Exp exp;

        public delegate void StartAction();
        public delegate void RefreshAction(Svframe f);
        public delegate void EndAction(Exp e);

        public StartAction StartHandler;
        public RefreshAction RefreshHandler;
        public EndAction EndHandler;

        private Thread refreshThread;

        public void Start(Scene scene, Mode mode)
        {
            this.scene = scene;
            this.mode = mode;

            exp = new Exp(scene.Name, mode.Name);
            exp.Tic();

            StartHandler();
            StartRefreshThread();
        }

        private void StartRefreshThread()
        {
            ThreadManager.StartThread(ThreadCluster.PlayerRefresh);
        }

        private void ShutRefreshThread()
        {

        }

        private void Refresh()
        {
            
        }

        public void End()
        {
            ShutRefreshThread();
            exp.Toc();
            EndHandler(this.exp);
        }

    }
}
