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
            LoadStyle();
            LoadText();
        }

        public static UDPSetting UDP;
        public static Dictionary<string, int> UDPOffset;
        public static Dictionary<string, Dictionary<string, string>> Style;
        public static Dictionary<string, Dictionary<string, string>> TextEn;
        public static Dictionary<string, Dictionary<string, string>> TextZh;

        private static void LoadUDP()
        {
            Dictionary<string, string> udpSetting = FileManger.GetUDPSetting();
            UDP = new UDPSetting();
            UDP.Port = Int32.Parse(udpSetting["Port"]);
            UDP.BufferSize =
                Int32.Parse(udpSetting["BufferSize"]);

            UDPOffset = FileManger.GetOffset();
        }

        private static void LoadStyle()
        {
            Style = FileManger.GetStyle();
        }

        private static void LoadText()
        {
            Dictionary<string, Dictionary<string, Dictionary<string,
                string>>> Text = FileManger.GetText();
            TextEn = Text["en"];
            TextZh = Text["zh"];
        }

        public static void Hit() { }
    }
}
