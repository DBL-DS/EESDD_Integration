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
         * It holds all Resources except static ones.
         */
        static CU()
        {
            MG_Set = new SettingManager();
            MG_User = new UserManager();
            MG_Game = new GameManager();
            MG_Page = new PageManager();
            Player = new Player(MG_Set.UDP, MG_Set.UDPOffset);
        }

        public static SettingManager MG_Set;
        public static UserManager MG_User;
        public static GameManager MG_Game;
        public static PageManager MG_Page;
        public static Player Player;

        public static void Hit() { }
    }
}
