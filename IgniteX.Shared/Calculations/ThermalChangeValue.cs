using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IgniteX.Shared.Calculations
{
    public class ThermalChangeValue
    {


        public ThermalChangeValue()
        {
          

        }

        /// <summary>
        /// 获取平均值
        /// </summary>
        /// <returns></returns>
        public string GetAvgResult()
        {
            return "平均值";
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        public string GetMaxResult()
        {
            return "最大值";
        }


        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <returns></returns>
        public string GetMinResult()
        {
           
            return "最小值";
        }

        /// <summary>
        /// 获取标准值
        /// </summary>
        /// <returns></returns>
        public string GetStandardResult()
        {
            return "标准值";
        }



        /// <summary>
        /// 将命令改为后台线程运行
        /// </summary>
        private DelegateCommand _backLoginCmd;
        public DelegateCommand BackLoginCmd => _backLoginCmd
            ?? (_backLoginCmd = new DelegateCommand(async () => await ExecuteBackLoginAsync()));

        private async Task ExecuteBackLoginAsync()
        {
            // 关闭当前窗口 
            Process.Start(Process.GetCurrentProcess().ProcessName);
            Application.Current.Shutdown();

            await Task.Run(() =>
            {
                // 关闭当前窗口
                Task.Delay(TimeSpan.FromSeconds(1));
                // 在STA线程上打开登录窗口
                Application.Current.Dispatcher.Invoke(() =>
                {
                  /*  LoginView loginWindow = new LoginView();
                    loginWindow.Show();*/
                }, System.Windows.Threading.DispatcherPriority.Normal, null);

            });
        }
    }
}
