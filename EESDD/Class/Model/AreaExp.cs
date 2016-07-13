using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class AreaExp
    {
        public AreaExp()
        {
            svframes = new List<Svframe>();
            brakes = new List<BrakeAct>();
            reacts = new List<Reaction>();
        }

        private List<Svframe> svframes;
        private EvaPara mean;
        private EvaPara var;
        private List<BrakeAct> brakes;
        private List<Reaction> reacts;
        private VissimPara vissim;
        private float score;

        public List<Svframe> Svframes
        {
            get { return svframes; }
        }

        public Svframe Start
        {
            get
            {
                if (svframes != null && svframes.Count != 0)
                    return svframes[0];
                else
                    return null;
            }
        }

        public Svframe End
        {
            get
            {
                if (svframes != null && svframes.Count != 0)
                    return svframes[svframes.Count - 1];
                else
                    return null;
            }
        }

        public EvaPara Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        public EvaPara Var
        {
            get { return var; }
            set { var = value; }
        }

        public List<BrakeAct> Brakes
        {
            get { return brakes; }
        }

        public BrakeAct LastBrake
        {
            get
            {
                if (brakes != null && brakes.Count != 0)
                    return brakes[brakes.Count - 1];
                else
                    return null;
            }
        }

        public List<Reaction> Reacts
        {
            get { return reacts; }
        }

        public Reaction LastReact
        {
            get
            {
                if (reacts != null && reacts.Count != 0)
                    return reacts[reacts.Count - 1];
                else
                    return null;
            }
        }

        public VissimPara Vissim
        {
            get { return vissim; }
            set { vissim = value; }
        }

        public float Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
