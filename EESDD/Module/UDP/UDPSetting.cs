using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Module.UDP
{
    class UDPSetting
    {
        public UDPSetting()
        {

        }

        private IPAddress serverIP;     // 服务器IP （备用）
        private int serverPort;         // 服务器监听端口 （备用） 
        private IPAddress ip;           // 本机IP
        private int port;               // 监听端口
        private int timeOut;            // 接收消息超时时长，单位：毫秒

        public delegate void Change();
        public Change OnChanged;

        public IPAddress ServerIP
        {
            get { return serverIP; }
            set 
            { 
                serverIP = value;
                OnChanged();
            }
        }

        public int ServerPort
        {
            get { return serverPort; }
            set 
            { 
                serverPort = value;
                OnChanged();
            }
        }

        public IPAddress IP
        {
            get { return ip; }
            set 
            {
                ip = value;
                OnChanged();
            }
        }

        public int Port
        {
            get { return port; }
            set 
            { 
                port = value;
                OnChanged();
            }
        }

        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value;}
        }
    }
}
