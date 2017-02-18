using EESDD.Class.Model;
using EESDD.Module.UDP;
using EESDD.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class UDPManager
    {
        public UDPManager(UDPSetting setting)
        {
            this.udpSetting = setting;
            this.udp = new UDP(setting);
        }

        private UDP udp;
        private UDPSetting udpSetting;
        private bool Testing;
        private bool Receiving;

        public ReceiveTimeOutAction ReceiveTimeOutAction
        {
            get { return udp.ReceiveTimeOutHandler; }
            set { udp.ReceiveTimeOutHandler = value; }
        }

        public bool? TestLink()
        {
            if (!Receiving)
            {
                Testing = true;

                udp.Open();
                byte[] message = udp.Receive();
                udp.Close();

                Testing = false;

                return message != null;
            }

            return null;
        }

        public void PrepareReceive()
        {
            Receiving = true;
            if (Testing)
                Thread.Sleep(udpSetting.TimeOut);
            udp.Open();
        }

        public byte[] Receive()
        {
            return udp.Receive();
        }

        public Svframe ReceiveFrame()
        {
            var bytes = Receive();
            if (bytes != null)
                return BytesConverter.ConvertWith<Svframe>(bytes, BytesToSvframe);
            else
                return null;
        }

        public void EndReceive()
        {
            udp.Close();
            Receiving = false;
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
