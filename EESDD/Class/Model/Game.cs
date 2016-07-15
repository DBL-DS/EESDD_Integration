namespace EESDD.Class.Model
{
    class Game
    {
        public Game(Scene scene, Mode mode)
        {
            this.scene = scene;
            this.mode = mode;
        }

        private Scene scene;
        private Mode mode;

        public Scene Scene
        {
            get { return scene; }
            set { scene = value; }
        }

        public Mode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
    }
}
