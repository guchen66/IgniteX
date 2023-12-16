using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IgniteX.Shared.Common
{
    public class TextString
    {
        public string GetPlcStatus()
        {
            return "未连接";
        }

        private readonly DispatcherTimer ShowTimer;  //声明一个计时器
        public string ShowCurrentTime()
        {
            //设置计时器
          /*  ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurrentTimer);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
            string date = DateTime.Now.DayOfWeek.ToString();  //当前日期
            if (date == "Monday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期一"; }
            else if (date == "Tuesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期二"; }
            else if (date == "Wednesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期三"; }
            else if (date == "Thursday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期四"; }
            else if (date == "Friday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期五"; }
            else if (date == "Saturday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期六"; }
            else if (date == "Sunday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期日"; }
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");  //当前时间*/

            return DateTime.Now.ToString(@"hh\:mm\:ss");
        }



        public string GetRunTime()
        {
            // 计算运行时间
            TimeSpan duration = DateTime.Now - startTime;

            // 格式化并返回运行时间
            return duration.ToString(@"hh\:mm\:ss");
        }
        public string GetStopTime() 
        {
            return "停止时间";
        }

        public string GetFaultText() 
        {
            return "故障时间";
        }

        private DateTime startTime;

        protected  void OnStartup(StartupEventArgs e)
        {        
           // 记录程序启动时间
            startTime = DateTime.Now;

            // 其他初始化逻辑...
        }

        protected  void OnExit(ExitEventArgs e)
        {
          

            // 获取运行时间并输出
            string runTime = GetRunTime();
            Console.WriteLine("程序运行时间：" + runTime);
        }

       

    }
}
