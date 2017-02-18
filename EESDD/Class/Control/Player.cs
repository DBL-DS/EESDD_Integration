using EESDD.Class.Model;
using EESDD.Module.UDP;
using EESDD.View;
using System.Collections.Generic;
using System.Threading;

namespace EESDD.Class.Control
{
    /*
     * Implementation of 
     */
    class Player
    {
        public Player()
        {
            recorder = new Recorder();
        }

        Recorder recorder;

        private bool _refreshEnable;

        public delegate void StartAction();
        public delegate void RefreshAction(Recorder recorder);
        public delegate void StopAction(Exp e);

        public StartAction StartHandler = null;
        public RefreshAction RefreshHandler = null;
        public StopAction StopHandler = null;

        private Thread refreshThread;

        public void Start(Scene scene, Mode mode)
        {
            recorder.Start(scene, mode);
            DefineRefreshThread();
            StartRefreshThread();
        }

        private void DefineRefreshThread()
        {
            refreshThread = ThreadManager.DefineThread(
                ThreadCluster.PlayerRefresh, Refresh);
        }

        private void StartRefreshThread()
        {
            StartHandler?.Invoke();

            _refreshEnable = true;
            ThreadManager.StartThread(ThreadCluster.PlayerRefresh);
        }

        private void StopRefreshThread()
        {
            _refreshEnable = false;
        }

        private void Refresh()
        {
            CU.MG_UDP.PrepareReceive();
            
            while (_refreshEnable)
            {
                Svframe frame = CU.MG_UDP.ReceiveFrame();

                if (frame != null)
                {
                    if (recorder.Record(frame))
                        RefreshHandler?.Invoke(recorder);
                }
                else
                    StopRefreshThread();
            }

            CU.MG_UDP.EndReceive();
        }

        public void End()
        {
            StopRefreshThread();
            Exp exp = recorder.Stop();
            StopHandler?.Invoke(exp);
        }


    }
}
