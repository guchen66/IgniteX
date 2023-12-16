using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models
{
    public class ElectronicData
    {
        public string ElectronicName { get; set; }  //电子元件
        public double Average { get; set; }     // 平均值
        public double Maximum { get; set; }     // 最大值
        public double Minimum { get; set; }     // 最小值  // 极差       最大值-最小值 Maximum-Minimum
        public double StandardDeviation { get; set; }    // 标准差
        public double Cp { get; set; }      // Cp
        public double Cpk { get; set; }     // Cpk

        public DateTime GreateTime { get; set; }

        // 构造函数
        public ElectronicData(string name,double avg, double max, double min, double sd, double cp, double cpk, DateTime greateTime)
        {
            Average = avg;
            Maximum = max;
            Minimum = min;
            StandardDeviation = sd;
            Cp = cp;
            Cpk = cpk;
            GreateTime = greateTime;
        }

        // 空构造函数
        public ElectronicData()
        {
            ElectronicName=string.Empty;
            Average = 0;
            Maximum = 0;
            Minimum = 0;
            StandardDeviation = 0;
            Cp = 0;
            Cpk = 0;
            GreateTime = DateTime.Now;
        }
    }
}
