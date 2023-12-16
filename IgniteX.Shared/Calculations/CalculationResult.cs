using IgniteX.Shared.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Calculations
{
    public class CalculationResult
    {
        private Queue<ElectronicData> _dataQueue = new Queue<ElectronicData>(100);
        public Dictionary<string, List<ElectronicData>> keyValuePairs = new Dictionary<string, List<ElectronicData>>();


        public ElectronicData[] _electronicDatas;

        public ElectronicData[] GetElectronicDatas()
        {
            return _electronicDatas;
        }
        public Queue<ElectronicData> ShowData()
        {
            // 假设获取到的测量值为measureValue
           // var result = new ElectronicData { MeasuredValue = measureValue };
           // _dataQueue.Enqueue(result);

            return _dataQueue;
        }



        // 计算数据的方法
        public void CalculateData(object obj = null)
        {
            // 获取计算数据

            // 计算平均值、最大值和最小值
          /*  double avg = GetResistanceData().Average();
            double max = GetResistanceData().Max();
            double min = GetResistanceData().Min();

            //计算极差
            double range = max - min;

            // 计算标准差  总体方差:((x1-x)^2+(x2-x)^2+(x3-x)^2)/3         标准差就是根号下方差
            //  double variance = GetResistanceData().Aggregate(0.0, (acc, x) => acc + Math.Pow(x - avg, 2)) / Data.Count;
            // double sd = Math.Sqrt(variance);
            double sd = GetResistanceData().PopulationStandardDeviation();


            // 计算Cp和Cpk
            double usl = 1;     // 设定上限规格
            double lsl = -1;    // 设定下限规格
            double cp = (usl - lsl) / (6 * sd);    // Cp指数            CP=(上限值-下限值)除以(6*标准差)

            //Cpk是一种过程能力指数，它表示了制程中实际产品品质分布的中心位置与规格限制之间的偏离程度
            //Cpk值越高，说明制程的品质控制越好，承受不良品率的能力也越强
            //公式  (上限-平均值)/(3*标准差) 和(平均值-下限)/(3*标准差)比较
            double cpk = Math.Min((avg - lsl) / (3 * sd), (usl - avg) / (3 * sd));   // Cpk指数

            DateTime dateTime = DateTime.Now;
            // 更新统计结果
            Result = new ElectronicData(GetName(), max, min, avg, sd, cp, cpk, dateTime);

            OnCalculationCompleted(Result);
            // 清空数据集合
            Data.Clear();*/
        }
    }
}
