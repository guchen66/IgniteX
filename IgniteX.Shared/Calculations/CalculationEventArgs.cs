using IgniteX.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Calculations
{
    public class CalculationEventArgs : EventArgs
    {
        public ElectronicData CalculationResult { get; set; }

        public CalculationEventArgs(ElectronicData result)
        {
            CalculationResult = result;
        }
    }
}
