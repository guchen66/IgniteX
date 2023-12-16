using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.UI.Models
{
    public class LimitData:BindableBase
    {
        public double Electronic { get; set; }
        public double Measured { get; set; }
        public double Upper { get; set; }
        public double Lower { get; set; }

        public LimitData(double electronic,double measured,double upper ,double lower)
        {
            Electronic = electronic;
            Measured = measured;
            Upper = upper;
            Lower = lower;
        }

        public LimitData()
        {
                
        }
    }
}
