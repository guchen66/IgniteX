using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgniteX.UI.ViewModels
{
    internal class ThermalChangeViewModel:BindableBase
    {

        private string _initRes;

        public string InitRes
        {
            get { return _initRes; }
            set { SetProperty<string>(ref _initRes, value); }
        }

        private string _finalRes;

        public string FinalRes
        {
            get { return _finalRes; }
            set { SetProperty<string>(ref _finalRes, value); }
        }

        private double _sliderValue;

        public double SliderValue
        {
            get { return _sliderValue; }
            set { SetProperty<double>(ref _sliderValue, value); }
        }


        private DelegateCommand _thermalChangeCommand;
        public DelegateCommand ThermalChangeCommand =>
            _thermalChangeCommand ?? (_thermalChangeCommand = new DelegateCommand(ExecuteCommand));

        private void ExecuteCommand()
        {
            double initialResistance, finalResistance;
            if (double.TryParse(InitRes, out initialResistance) &&
                double.TryParse(FinalRes, out finalResistance))
            {
                // 计算热瞬态电阻差值
                double resistanceDifference = Math.Abs(finalResistance - initialResistance);

                // 计算变化率
                double changeRate = (resistanceDifference / initialResistance) * 100;

                // 获取固定的变化率要求
                double fixedChangeRate = SliderValue;

                // 检查变化率是否满足要求
                bool isWithinRange = changeRate < fixedChangeRate;

                // 在检测数据中记录相关信息
                string detectionData = $"初始电阻: {initialResistance}，最终电阻: {finalResistance}，电阻差值: {resistanceDifference}，变化率: {changeRate}%，要求变化率小于: {fixedChangeRate}%，是否满足要求: {isWithinRange}";

                // 将检测数据记录到日志或其他地方
                RecordDetectionData(detectionData);
            }
            else
            {
                MessageBox.Show("请输入有效的电阻数值！");
            }
        }

        private void RecordDetectionData(string data)
        {
            // 在此处实现将检测数据记录到日志或其他逻辑
            // 可以将数据写入文件、数据库中或发送到服务器等
            // 这里仅作示例，您需要根据实际需求进行实现
            Console.WriteLine(data);
        }

        /// <summary>
        /// 右侧窗口是否展开
        /// </summary>
        private bool isRightDrwerOpen;

        public bool IsRightDrwerOpen
        {
            get { return isRightDrwerOpen; }
            set { isRightDrwerOpen = value; RaisePropertyChanged(); }
        }



        private DelegateCommand<string> _addCommand;
        public DelegateCommand<string> AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand<string>(ExecuteAddCommand));

        private void ExecuteAddCommand(string parameter)
        {
            IsRightDrwerOpen = true;
        }

    }
}
