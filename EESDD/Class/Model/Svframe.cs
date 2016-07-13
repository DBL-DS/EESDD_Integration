namespace EESDD.Class.Model
{
    class Svframe
    {
        private float time;         // 行驶时间
        private float x;            // 车的坐标x
        private float y;            // 车的坐标y
        private float speed;        // 速度
        private float acc;          // 加速度
        private float stwAngle;     // 方向盘转角
        private float offset;       // 偏离道路中性线距离
        private float brake;        // 刹车踏板
        private float distance;     // 总行驶距离
        private float braking;      // 刹车状态 0-否 1-是
        private float reacting;     // 反应状态 0-未进入反应阶段或反应结束 1-反应中
        private float area;         // 区域标记
        private float farToFront;   // 与前车的距离
        private float lane;         // 车道
        private float trLight;      // 信号灯

        public float Time
        {
            get { return time; }
            set { time = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public float StwAngle
        {
            get { return stwAngle; }
            set { stwAngle = value; }
        }

        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public float Brake
        {
            get { return brake; }
            set { brake = value; }
        }

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public float Braking
        {
            get { return braking; }
            set { braking = value; }
        }

        public float Reacting
        {
            get { return reacting; }
            set { reacting = value; }
        }

        public float Area
        {
            get { return area; }
            set { area = value; }
        }

        public float FarToFront
        {
            get { return farToFront; }
            set { farToFront = value; }
        }

        public float Lane
        {
            get { return lane; }
            set { lane = value; }
        }


        public float TrLight
        {
            get { return trLight; }
            set { trLight = value; }
        }
    }
}
