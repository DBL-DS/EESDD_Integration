using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Module.UDP
{
    class UDPSetting
    {
        public UDPSetting()
        {
            setDefault();
        }

        private IPAddress serverIP;     // 服务器IP （备用）
        private int serverPort;         // 服务器监听端口 （备用） 
        private IPAddress ip;           // 本机IP
        private int port;               // 监听端口
        private int timeOut;            // 接收、发送消息超时时长，单位：毫秒
        private int bufferSize;         // 接受的字节数，默认为0表示所有

        public delegate void ClientChangeHandler(UDPSetting setting);
        public ClientChangeHandler ClientChangeAction;

        public IPAddress ServerIP
        {
            get { return serverIP; }
            set 
            { 
                serverIP = value;
            }
        }

        public int ServerPort
        {
            get { return serverPort; }
            set 
            { 
                serverPort = value;
            }
        }

        public IPAddress IP
        {
            get { return ip; }
            set 
            {
                ip = value;
                ClientChange();
            }
        }

        public int Port
        {
            get { return port; }
            set 
            { 
                port = value;
                ClientChange();
            }
        }

        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value;}
        }

        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }

        private void setDefault()
        {
            serverIP = IPAddress.Broadcast;
            serverPort = 9999;
            ip = GetLocalIP();
            port = 9999;
            timeOut = 3000;
        }

        private IPAddress GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void ClientChange()
        {
            ClientChangeAction?.Invoke(this);
        }
    }
}
