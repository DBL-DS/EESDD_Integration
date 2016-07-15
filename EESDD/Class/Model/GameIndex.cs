using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class GameIndex
    {
        public GameIndex(string scene, string mode)
        {
            this.scene = scene;
            this.mode = mode;
        }

        string scene;
        string mode;

        public string Scene
        {
            get { return scene; }
            set { value = scene; }
        }

        public string Mode
        {
            get { return mode; }
            set { value = mode; }
        }
    }
}
