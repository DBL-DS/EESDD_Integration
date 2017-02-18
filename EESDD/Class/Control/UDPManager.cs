using EESDD.Module.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class UDPManager
    {
        public UDPManager(UDPSetting setting)
        {
            this.udpSetting = setting;
            this.UDP = new UDP(setting);
        }

        private UDPSetting udpSetting;
        
        public UDP UDP { get; set; }
    }
}
