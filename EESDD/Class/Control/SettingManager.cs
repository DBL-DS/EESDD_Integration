﻿using EESDD.Module.UDP;
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
            LoadUDP();
            LoadStyle();
            LoadText();
        }

        public UDPSetting UDP;
        public Dictionary<string, int> UDPOffset;
        public Dictionary<string, Dictionary<string, string>> Style;
        public Dictionary<string, Dictionary<string, string>> TextEn;
        public Dictionary<string, Dictionary<string, string>> TextZh;

        private void LoadUDP()
        {
            Dictionary<string, string> udpSetting = FileManager.GetUDPSetting();
            UDP = new UDPSetting();
            UDP.Port = Int32.Parse(udpSetting["Port"]);
            UDP.BufferSize =
                Int32.Parse(udpSetting["BufferSize"]);

            UDPOffset = FileManager.GetOffset();
        }

        private void LoadStyle()
        {
            Style = FileManager.GetStyle();
        }

        private void LoadText()
        {
            Dictionary<string, Dictionary<string, Dictionary<string,
                string>>> Text = FileManager.GetText();
            TextEn = Text["en"];
            TextZh = Text["zh"];
        }
    }
}
