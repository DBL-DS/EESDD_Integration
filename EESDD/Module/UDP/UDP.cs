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
            this.setting = setting;
            setting.ClientChangeAction 
                += new UDPSetting.ClientChangeHandler(InitClient);
            InitUDP(setting);
        }

        private UDPSetting setting;
        private UdpClient client;
        private IPEndPoint clientEndPoint;  // 本机一律视为客户端
        private IPEndPoint serverEndPoint;  // 接收目标或发送目标一律视为服务端

        public delegate void ReceiveTimeOutHandler();
        public delegate void SendTimeOutHandler();

        public ReceiveTimeOutHandler ReceiveTimeOutAction;
        public SendTimeOutHandler SendTimeOutAction;

        private void InitUDP(UDPSetting setting)
        {
            InitClient(setting);
            InitServer(setting);
            setting.ClientChangeAction 
                = new UDPSetting.ClientChangeHandler(InitClient);
        }

        private void InitClient(UDPSetting setting)
        {
            if (client != null)
                client.Close();
            client = new UdpClient(setting.Port);
            client.Client.ReceiveTimeout = setting.TimeOut;
            client.Client.SendTimeout = setting.TimeOut;
            clientEndPoint = new IPEndPoint(setting.IP, setting.Port);
        }

        private void InitServer(UDPSetting setting)
        {
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
                if (SendTimeOutAction != null)
                    SendTimeOutAction();
                return false;
            }
        }

        public byte[] Receive()
        {
            byte[] message = null;
            try
            {
                message = client.Receive(ref serverEndPoint);
            }
            catch
            {
                if (ReceiveTimeOutAction != null)
                    ReceiveTimeOutAction();
            }

            setting.ServerIP = serverEndPoint.Address;
            setting.ServerPort = serverEndPoint.Port;

            return message;
        }

        public void Shut()
        {
            client.Close();
        }
    }
}
