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
        }

        private Scene scene;
        private Mode mode;
        private Exp exp;

        private bool _refreshEnable;

        public delegate void StartAction();
        public delegate void RefreshAction(Svframe f);
        public delegate void EndAction(Exp e);

        public StartAction StartHandler;
        public RefreshAction RefreshHandler;
        public EndAction EndHandler;
        public ReceiveTimeOutAction ReceiveTimeOutHandler;

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
            _refreshEnable = true;
            ThreadManager.StartThread(ThreadCluster.PlayerRefresh);
        }

        private void ShutRefreshThread()
        {
            _refreshEnable = false;
        }

        private void Refresh()
        {
            UDP udp = new UDP(SettingManger.UDP);

            udp.ReceiveTimeOutHandler = this.ReceiveTimeOutHandler;

            while (_refreshEnable)
            {
                byte[] bytes = udp.Receive();
                Svframe frame = 
                    BytesConverter.ConvertWith<Svframe>(bytes,
                    this.BytesToSvframe);

                RefreshHandler(frame);
            }

            udp.Close();
        }

        public void End()
        {
            ShutRefreshThread();
            exp.Toc();
            EndHandler(this.exp);
        }

        private Svframe BytesToSvframe(byte[] bytes)
        {
            float[] floats = BytesConverter.ToFloatArray(bytes);
            Svframe frame = new Svframe();

            foreach (var item in SettingManger.UDPOffset)
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
