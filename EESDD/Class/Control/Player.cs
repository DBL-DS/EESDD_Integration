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
            UDP udp = CU.MG_UDP.UDP;
            udp.Open();
            
            while (_refreshEnable)
            {
                byte[] bytes = udp.Receive();

                if (bytes != null)
                {
                    Svframe frame =
                        BytesConverter.ConvertWith<Svframe>(bytes,
                        this.BytesToSvframe);

                    if (recorder.Record(frame))
                        RefreshHandler?.Invoke(recorder);
                    //recorder.Record(frame);
                    //RefreshHandler?.Invoke(recorder);
                }
                else
                {
                    StopRefreshThread();
                }
            }

            udp.Close();
        }

        public void End()
        {
            StopRefreshThread();
            Exp exp = recorder.Stop();
            StopHandler?.Invoke(exp);
        }

        /*
         * Transfer UDP message from bytes to a svframe
         */
        private Svframe BytesToSvframe(byte[] bytes)
        {
            float[] floats = BytesConverter.ToFloatArray(bytes);
            Svframe frame = new Svframe();

            foreach (var item in CU.MG_Set.UDPOffset)
            {
                var name = item.Key;
                var offset = item.Value;
                frame.GetType().GetProperty(name)
                    .SetValue(frame, floats[item.Value]);
            }

            return frame;
        }

    }
}
