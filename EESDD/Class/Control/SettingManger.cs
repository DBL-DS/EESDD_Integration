using EESDD.Module.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    static class SettingManger
    {
        static SettingManger()
        {
            LoadUDP();
        }

        public static UDPSetting UDP;
        public static Dictionary<string, int> UDPOffset;

        private static void LoadUDP()
        {
            var udpSetting = FileManger.GetUDPSetting();
            UDP = new UDPSetting();
            UDP.Port = Convert.ToInt32(udpSetting["Port"]);
            UDP.BufferSize = 
                Convert.ToInt32(udpSetting["BufferSize"]);

            UDPOffset = FileManger.GetOffset();
        }
    }
}
