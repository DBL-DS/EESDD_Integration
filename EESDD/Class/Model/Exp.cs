using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Exp
    {
        public Exp()
        {
            areas = new List<AreaExp>();
        }

        private DateTime start;                 // 体验开始时间
        private DateTime end;                   // 体验结束时间
        private Game game;                      // 体验的场景、模式
        private List<AreaExp> areas;            // 按区域划分的体验集合

        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        public Game Game
        {
            get { return game; }
            set { game = value; }
        }

        public List<AreaExp> Areas
        {
            get { return areas; }
        }

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

                if (areas.Count == 0)
                    areas.Add(value);
                else
                    areas[0] = value;
            }
        }

    }
}
