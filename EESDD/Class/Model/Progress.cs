using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public class Progress
    {
        public Progress(float value, string info)
        {
            Value = value;
            Info = info;
        }

        public float Value { get; set; }
        public string Info { get; set; }
        public string Percent { get { return (Value * 100).ToString("F0"); } }
    }
}
