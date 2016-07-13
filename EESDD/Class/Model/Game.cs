namespace EESDD.Class.Model
{
    class Game
    {
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
