using EESDD.Class.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.View
{
    static class CentralUnit
    {
        static CentralUnit()
        {
            MG_User = new UserManager();
            MG_Game = new GameManager();
            Player = new Player();
        }

        public static UserManager MG_User;
        public static GameManager MG_Game;
        public static Player Player;

        public static void Hit() { }
    }
}
