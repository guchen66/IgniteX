
using IgniteX.Shared.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.UI.ViewModels
{
    public class ResistanceViewModel : BindableBase
    {
        private ObservableCollection<double> _data;   // 数据
        private ElectronicData _result;    // 统计信息

        public DelegateCommand<object> CalculateDataCommand { get; set; }   // 计算数据的命令

        // 数据集合
        public ObservableCollection<double> Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        // 统计结果
        public ElectronicData Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        // 构造函数
        public ResistanceViewModel()
        {
            Data = new ObservableCollection<double>();
            Result = new ElectronicData();

            // 初始化命令
            CalculateDataCommand = new DelegateCommand<object>(CalculateData, CanCalculateData)
                .ObservesProperty(() => Data.Count);
        }

        // 可执行命令的判断条件
        private bool CanCalculateData(object obj)
        {
            return Data != null && Data.Count >= 100;
        }

        // 计算数据的方法
        private void CalculateData(object obj)
        {
            // 计算平均值、最大值和最小值
            double avg = Data.Average();
            double max = Data.Max();
            double min = Data.Min();

            // 计算标准偏差
            double variance = Data.Aggregate(0.0, (acc, x) => acc + Math.Pow(x - avg, 2)) / Data.Count;
            double sd = Math.Sqrt(variance);

            // 计算Cp和Cpk
            double usl = 1;     // 设定上限规格
            double lsl = -1;    // 设定下限规格
            double cp = (usl - lsl) / (6 * sd);    // Cp指数
            double cpk = Math.Min((avg - lsl) / (3 * sd), (usl - avg) / (3 * sd));   // Cpk指数
            DateTime dateTime = DateTime.Now;
            // 更新统计结果
            Result = new ElectronicData("",avg, max, min, sd, cp, cpk, dateTime);

            // 清空数据集合
            Data.Clear();
        }

        /// <summary>
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
            var avg = Data.Average();
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
        }


    }
}
