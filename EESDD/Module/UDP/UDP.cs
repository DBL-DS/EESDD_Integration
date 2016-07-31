using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Module.UDP
{
    class UDP
    {
        public UDP(UDPSetting setting)
        {
            setting.OnChanged += new UDPSetting.Change(InitUDP);
            this.setting = setting;
        }

        private UDPSetting setting;
        private UdpClient client;
        private IPEndPoint clientEndPoint;
        private IPEndPoint serverEndPoint;

        private void InitUDP()
        {
            client = new UdpClient(setting.Port);
            client.Client.ReceiveTimeout = setting.TimeOut;
            clientEndPoint = new IPEndPoint(setting.IP, setting.Port);
            serverEndPoint = new IPEndPoint(setting.ServerIP, setting.ServerPort);
        }

        public bool Send(byte[] message)
        {
            try
            {
                int count = client.Send(message, 
                    message.Length, serverEndPoint);

                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public byte[] Receive()
        {
            byte[] message = client.Receive(ref serverEndPoint);

            return message;
        }



    }
}
