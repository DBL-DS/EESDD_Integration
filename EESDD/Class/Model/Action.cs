using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    abstract class Action
    {
        private Svframe start;
        private Svframe end;

        public Svframe Start
        {
            get { return start; }
            set { start = value; }
        }

        public Svframe End
        {
            get { return end; }
            set { end = value; }
        }

        public float Time
        {
            get { return end.Time - start.Time; }
        }

        public float Distance
        {
            get { return end.Distance - start.Distance; }
        }
    }
}
