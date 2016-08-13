using EESDD.Class.Model;
using EESDD.Module.UDP;
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

            recorder = new Recorder();
        }

        Recorder recorder;

        private bool _refreshEnable;

        public delegate void StartAction();
        public delegate void RefreshAction(Recorder recorder);
        public delegate void StopAction(Exp e);

        public StartAction StartHandler;
        public RefreshAction RefreshHandler;
        public StopAction StopHandler;
        public ReceiveTimeOutAction ReceiveTimeOutHandler;

        private Thread refreshThread;

        public void Start(Scene scene, Mode mode)
        {
            recorder.Start(scene, mode);
            StartRefreshThread();
        }

        private void StartRefreshThread()
        {
            StartHandler();

            _refreshEnable = true;
            ThreadManager.StartThread(ThreadCluster.PlayerRefresh);
        }

        private void StopRefreshThread()
        {
            _refreshEnable = false;
        }

        private void Refresh()
        {
            UDP udp = new UDP(SettingManager.UDP);

            udp.ReceiveTimeOutHandler = this.ReceiveTimeOutHandler;

            while (_refreshEnable)
            {
                byte[] bytes = udp.Receive();

                if (bytes != null)
                {
                    Svframe frame =
                        BytesConverter.ConvertWith<Svframe>(bytes,
                        this.BytesToSvframe);

                    recorder.Record(frame);

                    if (RefreshHandler != null)
                        RefreshHandler(recorder);
                }
                else
                    StopRefreshThread();
            }

            udp.Close();
        }

        public void End()
        {
            StopRefreshThread();
            Exp exp = recorder.Stop();
            StopHandler(exp);
        }

        private Svframe BytesToSvframe(byte[] bytes)
        {
            float[] floats = BytesConverter.ToFloatArray(bytes);
            Svframe frame = new Svframe();

            foreach (var item in SettingManager.UDPOffset)
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
