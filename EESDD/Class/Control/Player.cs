using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class Player
    {
        private Scene scene;
        private Mode mode;

        public void SelectScene(Scene scene)
        {
            this.scene = scene;
        }

        public void SelectMode(Mode mode)
        {
            this.mode = mode;
        }

        public bool PlayStart()
        {
            return false;
        }

    }
}
