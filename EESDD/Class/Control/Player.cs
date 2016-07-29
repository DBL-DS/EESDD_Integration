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
        private Exp exp;

        public event EventHandler StartAction;
        public event EventHandler RefreshAction;

        public void Start(Scene scene, Mode mode)
        {
            this.scene = scene;
            this.mode = mode;

            exp = new Exp(scene.Name, mode.Name);
            exp.Tic();

            StartAction(this, new EventArgs());
        }

        public void End()
        {
            exp.Toc();
        }

    }
}
