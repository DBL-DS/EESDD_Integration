using EESDD.Class.Model;
using MathNet.Numerics.Statistics;
using System.Linq;

namespace EESDD.Class.Control
{
    /*
     * Evaluator is to evaluate an experience, do some calculation
     */
    class Evaluator
    {
        private Exp exp;

        public void Evaluate(Exp exp)
        {
            this.exp = exp;
            exp.Evaluated = false;

            foreach (var area in exp.Areas)
            {
                CalcuMeanAndVar(area);
                CalcuScore(area);
            }

            exp.Evaluated = true;
        }

        private void CalcuMeanAndVar(AreaExp area)
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

        private void CalcuScore(AreaExp area)
        {

        }
    }
}
