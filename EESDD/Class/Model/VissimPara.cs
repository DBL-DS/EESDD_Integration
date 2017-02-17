using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public class VissimPara
    {
        private float delay;
        private float queueLenAvg;
        private float speedAvg;

        public float Delay
        {
            get { return delay; }
            set { delay = value; }
        }

        public float QueueLenAvg
        {
            get { return queueLenAvg; }
            set { queueLenAvg = value; }
        }

        public float SpeedAvg
        {
            get { return speedAvg; }
            set { speedAvg = value; }
        }
    }
}
