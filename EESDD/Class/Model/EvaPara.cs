using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class EvaPara
    {
        private float speed;        // 速度
        private float acc;          // 加速度
        private float stwAngle;     // 方向盘转角
        private float offset;       // 偏离道路中性线距离
        private float brake;        // 刹车踏板
        private float farToFront;   // 与前车的距离

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

        public float FarToFront
        {
            get { return farToFront; }
            set { farToFront = value; }
        }
    }
}
