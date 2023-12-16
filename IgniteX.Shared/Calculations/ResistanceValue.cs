using IgniteX.Shared.Models;
using MathNet.Numerics.Statistics;
using Prism.Commands;
using Prism.Mvvm;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Calculations
{
    public class ResistanceValue:BindableBase
    {
        private Queue<ElectronicData> _dataQueue = new Queue<ElectronicData>(100);
        public Dictionary<string, List<ElectronicData>> keyValuePairs = new Dictionary<string, List<ElectronicData>>();
        // 将队列暴露为只读属性
        public Queue<ElectronicData> DataQueue => _dataQueue;

        IgniteTubeService db = new IgniteTubeService();
        private ElectronicData _result;    // 统计信息
        public ObservableCollection<ElectronicData> _calculationData=new ObservableCollection<ElectronicData>();   // 计算的数据
        public ObservableCollection<ElectronicData> Data
        {
            get { return _calculationData; }
            set { SetProperty(ref _calculationData, value); }
        }

        // 统计结果
        public ElectronicData Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }
        public event EventHandler<CalculationEventArgs> CalculationCompleted;

        private void OnCalculationCompleted(ElectronicData result)
        {
            CalculationCompleted?.Invoke(this, new CalculationEventArgs(result));
        }

        // 测得值数据
        public List<double> GetResistanceData()
        {
            var result = db.QueryList().Select(e => e.InsulResistive).ToList();
            return result;
        }

       

        // 可执行命令的判断条件
        public bool CanCalculateData(object obj)
        {
            return Data != null && Data.Count >= 100;
        }
        // 计算数据的方法
        public void CalculateData(object obj,Action backCall)
        {
            // 获取计算数据
            if (GetResistanceData().Count == 0) 
            {
                OnCalculationCompleted(new ElectronicData());
            }
            else
            {
                // 计算平均值、最大值和最小值
                double avg = GetResistanceData().Average();
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
                Data.Clear();
            }
         
          
        }


        public string GetName()
        {
            return "绝缘电阻";
        }

        /// <summary>
        /// 获取上下限值
        /// </summary>
        /// <param name="upperResult"></param>
        /// <returns></returns>
        public int GetUpper(int upperResult)
        {
            return upperResult;
        }

        public int GetLowwer(int lowerResult)
        {
            return lowerResult;
        }







       /* /// <summary>
        /// 获取平均值
        /// </summary>
        /// <returns></returns>
        public double GetAvgResult()
        {
            return Data.Average();
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        public double GetMaxResult()
        {
            return Data.Max();
        }


        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <returns></returns>
        public double GetMinResult()
        {
            return Data.Min();
        }

        /// <summary>
        /// 获取标准值
        /// </summary>
        /// <returns></returns>
        public double GetStandardResult()
        {
            var avg=Data.Average();
            double variance = Data.Aggregate(0.0, (acc, x) => acc + Math.Pow(x - avg, 2)) / Data.Count;
            double sd = Math.Sqrt(variance);
            return sd;
        }

        /// <summary>
        /// 获取偏差
        /// </summary>
        /// <returns></returns>
        public string GetDeviation()
        {
            return "偏差";
        }

        /// <summary>
        /// 获取Cp
        /// </summary>
        /// <returns></returns>
        public string GetCpResult()
        {
            return "Cp";
        }

        /// <summary>
        /// 获取Cpk
        /// </summary>
        /// <returns></returns>
        public string GetCpkResult()
        {
            return "Cpk";
        }*/


        public List<double> TraceData(List<double> lists, Func<double, bool> func)
        {
            List<double> doubles = new List<double>();
            foreach (var item in lists)
            {
                if (func(item))
                {
                    doubles.Add(item);
                }
               
            }
            return doubles;
        }
       
    }
}
