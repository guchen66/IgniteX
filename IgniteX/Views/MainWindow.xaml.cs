
using IgniteX.Shared.Events;
using IgniteX.UI.Views;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace IgniteX.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int clickCount = 0;
        private DispatcherTimer timer;
        private readonly DispatcherTimer ShowTimer;  //声明一个计时器
        public MainWindow()
        {
            InitializeComponent();
            //设置计时器
            ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurrentTimer);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            };

         
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(300); // 设置双击事件的时间阈值为300毫秒
            timer.Tick += Timer_Tick;

        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (clickCount == 0)
                {
                    timer.Start(); // 启动计时器
                }
                clickCount += 1;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (clickCount >= 2)
            {
                this.WindowState = this.WindowState == WindowState.Maximized ?
                 WindowState.Normal : WindowState.Maximized;

            }
     
            clickCount = 0; // 重置计数器
            timer.Stop(); // 停止计时器
        }

        // 实时显示日期时间
        private void ShowCurrentTimer(object sender, EventArgs e)
        {
            string date = DateTime.Now.DayOfWeek.ToString();  //当前日期
            if (date == "Monday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期一"; }
            else if (date == "Tuesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期二"; }
            else if (date == "Wednesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期三"; }
            else if (date == "Thursday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期四"; }
            else if (date == "Friday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期五"; }
            else if (date == "Saturday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期六"; }
            else if (date == "Sunday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期日"; }
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");  //当前时间
        }

    }
}
