using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public enum ExpType
    {
        Cancel,  /* For MessageBox */
        Normal,
        CarCrash_Normal,
        CarCrash_Accident,   /* Not Evaluate */
        Operator_Accident,   /* Not Evaluate */
        Others              /* Not Evaluate */
    }
    public class Exp
    {
        public Exp(string scene, string mode)
        {
            this.scene = scene;
            this.mode = mode;
            this.evaluated = false;
        }

        public Exp()
        {
            evaluated = false;
        }

        private DateTime startTime;                 // exp start time
        private DateTime endTime;                   // exp end time
        private string scene;                       // scene name of this exp
        private string mode;                        // mode name of this exp
        private List<AreaExp> areas;                // 按区域划分的体验集合
        private bool evaluated;
        private ExpType expType;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public string Scene
        {
            get { return scene; }
            set { scene = value; }
        }

        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public List<AreaExp> Areas
        {
            get { return areas; }
            set { areas = value; }
        }

        public bool Evaluated
        {
            get { return evaluated; }
            set { evaluated = value; }
        }

        public ExpType ExpType
        {
            get { return expType; }
            set { expType = value; }
        }

        [JsonIgnore]
        private int AreaCount
        {
            get {
                if (areas != null)
                    return areas.Count;
                else
                    return 0;
            }
        }

        [JsonIgnore]
        public AreaExp TotalArea
        {
            get
            {
                if (areas != null && areas.Count != 0)
                    return areas[0];
                else
                    return null;
            }
            set
            {
                if (areas == null)
                    areas = new List<AreaExp>();

                areas[0] = value;
            }
        }

        public void Tic()
        {
            startTime = DateTime.Now;
        }

        public void Toc()
        {
            endTime = DateTime.Now;
        }
    }
}
