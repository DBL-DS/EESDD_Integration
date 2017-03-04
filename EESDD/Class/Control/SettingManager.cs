using EESDD.Module.UDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class SettingManager
    {
        public SettingManager()
        {
            LoadAppSetting();
            LoadUDP();
            LoadStyle();
            LoadText();
        }

        public Dictionary<string, Dictionary<string, string>> App;
        public UDPSetting UDP;
        public Dictionary<string, int> UDPOffset;
        public Dictionary<string, Dictionary<string, string>> Style;
        private Dictionary<string, Dictionary<string, string>> TextEn;
        private Dictionary<string, Dictionary<string, string>> TextZh;

        private void LoadUDP()
        {
            UDP = new UDPSetting();
            UDP.Port = Int32.Parse(App["udp"]["port"]);
            UDP.BufferSize =
                Int32.Parse(App["udp"]["port"]);

            UDPOffset = FileManager.GetOffset();
        }

        private void LoadAppSetting()
        {
            App = FileManager.GetAppSetting();
        }

        private void LoadStyle()
        {
            Style = FileManager.GetStyle();
        }

        private void LoadText()
        {
            Dictionary<string, Dictionary<string, Dictionary<string,
                string>>> TextCluster = FileManager.GetText();
            TextEn = TextCluster["en"];
            TextZh = TextCluster["zh"];
        }

        public Dictionary<string, Dictionary<string,string>> Text
        {
            get
            {
                switch (App["style"]["lang"])
                {
                    case "zh":
                        return TextZh;
                    case "en":
                        return TextEn;
                }
                    return TextZh;
            }
        }

        public void SaveAppSetting()
        {
            //TODO
        }

        public void SaveStyleSetting()
        {
            //TODO
        }
    }
}
