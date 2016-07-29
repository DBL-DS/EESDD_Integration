using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class BrakeAct:Action
    {
        public float BrakeDistanceNow(Svframe frame)
        {
            return BrakeDistanceNow(frame.Distance);
        }
        public float BrakeDistanceNow(float distance)
        {
            return distance - Start.Distance;
        }

        public float EndAct(Svframe frame)
        {
            this.End = frame;
            return BrakeDistanceNow(frame);
        }
    }
}
