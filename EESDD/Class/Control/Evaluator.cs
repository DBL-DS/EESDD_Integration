using EESDD.Class.Model;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    class Evaluator
    {
        private Exp exp;

        public void Evaluate(Exp exp)
        {
            this.exp = exp;
            exp.Evaluated = false;

            foreach (var area in exp.Areas)
            {
                CacuMeanAndVar(area);
                CacuScore(area);
            }

            exp.Evaluated = true;
        }

        private void CacuMeanAndVar(AreaExp area)
        {
            area.Var = new EvaPara();
            area.Mean = new EvaPara();
            var frames = area.Svframes;

            foreach (var attr in area.Var.GetType().GetProperties())
            {
                var attrName = attr.Name;
                var array = frames.Select<Svframe, float>
                    (x => 
                        (float)x.GetType().GetProperty(attrName)
                        .GetValue(x, null))
                    .ToArray();

                attr.SetValue(area.Mean, (float)Statistics.Mean(array));
                attr.SetValue(area.Var, (float)Statistics.Variance(array));
            }
        }

        private void CacuScore(AreaExp area)
        {

        }
    }
}
