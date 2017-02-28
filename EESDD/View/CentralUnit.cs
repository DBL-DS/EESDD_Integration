using EESDD.Class.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.View
{
    static class CU
    {
        /* 
         * This static Class is the center of the System.
         * It holds all Resources except Thread and File.
         */
        static CU()
        {
            MG_Set  = new SettingManager();
            MG_User = new UserManager();
            MG_Exp = new ExpManager();
            MG_Game = new GameManager();
            MG_Page = new PageManager();
            MG_UDP = new UDPManager(MG_Set.UDP);
            Player  = new Player();
        }

        public static SettingManager MG_Set;
        public static UserManager MG_User;
        public static ExpManager MG_Exp;
        public static GameManager MG_Game;
        public static PageManager MG_Page;
        public static UDPManager MG_UDP;
        public static Player Player;

        public static void Hit() { }
    }
}
