using IgniteX.Models;
using IgniteX.PlcLib.Services;
using IgniteX.Shared;
using IgniteX.Shared.Commands;
using IgniteX.Shared.Common;
using IgniteX.Shared.Converters;
using IgniteX.Shared.Events;
using IgniteX.UI.Views;
using IgniteX.UI.Views.Dialogs;
using IgniteX.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IgniteX.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ISiemensPlcFactoryService _siemensPlcFactoryService;
        private readonly IProProcessService _proProcessService;
        private readonly ILog _logger;
        public AppData appData { get; set; } = AppData.Instance;
        IRegionManager _regionManager;//区域管理
        private readonly IEventAggregator _eventAggregator;
        IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
        public MainWindowViewModel(IProProcessService proProcessService,IRegionManager regionManager, IRegionNavigationJournal navigationJournal,IEventAggregator eventAggregator, ILog logger)
        {
            _proProcessService = proProcessService;
            _logger = logger;
            _regionManager = regionManager;
            _navigationJournal = navigationJournal;
            _eventAggregator = eventAggregator;
            // _regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
           // regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
          //  regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
          //  regionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            NavicateCommand = new DelegateCommand<string>(ExecuteNavicate);
            CloseCommand = new DelegateCommand(ExecuteClose);
            IssuedOrderCmd = new DelegateCommand(ExecuteIssued);
            DragMoveCommand = new DelegateCommand(ExecuteDrag);
            InitializingCommand = new DelegateCommand(async () =>await ExecuteLoading()); ;
        }

        private string _orderNumber;
        public string OrderNumber
        {
            get { return _orderNumber; }
            set { SetProperty<string>(ref _orderNumber, value); }
        }


        #region 命令
        public ICommand InitializingCommand { get;set; }
        public ICommand NavicateCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand IssuedOrderCmd { get; set; }
        public ICommand DragMoveCommand { get; set; }
        #endregion

        #region  方法

        private void ExecuteNavicate(string menuTitle)
        {
            //switch ((MenuTitle)Enum.Parse(typeof(MenuTitle), menuTitle))
            switch (menuTitle)
            {
                case "首页":
                    Navigate("HomeView");
                    break;
                case "点火管管理":
                    Navigate("IgniteTubeView");
                    break;
                case "设备状态":
                    Navigate("DeviceStateView");
                    break;
               
                case "Plc列表":
                    Navigate("PlcConfigView");
                    break;
                case "历史报警":
                    Navigate("AlarmHistory");
                    break;
                case "报警记录":
                    Navigate("AlarmLogging");
                    break;
                case "配方列表":
                    Navigate("RepiceList");
                    break;
                case "配方参数":
                    Navigate("RepiceArgs");
                    break;
                case "本地日志":
                    ExecuteOpenLog();
                    break;
            }
        }

        //导航
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
                {
              //  _logger.Info("导航到"+ navigatePath);
                    _navigationJournal = arg.Context.NavigationService.Journal;
                });
        }

        //关闭
        private void ExecuteClose()
        {
            Application.Current.Shutdown();
        }

        private void ExecuteIssued()
        {
            if (OrderNumber == null) 
            {
                MessageBox.Show("请先输入订单号");
            }
        }

        //移动
        private void ExecuteDrag()
        {
           Application.Current.MainWindow.DragMove();

        }
 

        private async Task ExecuteLoading()
        {
            await _proProcessService.GetWorkingOrderInfo();
           // _siemensPlcFactoryService.InitFactory();//初始化工厂  _siemensPlcFactoryService.StartPLC();//连接所有PLC,测试时候先不用
         //   WorkingProcess workingProcess = new WorkingProcess();//创建过程处理对象         在主界面创建之前初始化的时候，不停的与PLC通信，触发事件
          //  workingProcess.Init();
        }
        #endregion

        #region 查看本地日志
        /// <summary>
        /// 查看本地日志
        /// </summary>
        private DelegateCommand _openLog;
        public DelegateCommand OpenLog =>
            _openLog ?? (_openLog = new DelegateCommand(ExecuteOpenLog));

        private void ExecuteOpenLog()
        {
           
            // 从配置中获取日志文件夹路径
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            try
            {
                if (string.IsNullOrWhiteSpace(logDirectory))
                {
                    // 如果日志文件夹路径为空（可能是配置里没有定义），则弹出一个错误的对话框
                    MessageBox.Show("NLog配置错误.", "Error");
                    return;
                }

                // 检查日志文件夹是否存在和可访问
                if (!Directory.Exists(logDirectory))
                {
                    // 日志文件夹路径不正确，弹出错误对话框
                    MessageBox.Show($"Cannot access log directory: {logDirectory}", "Error");
                    return;
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"\"{logDirectory}\"",
                    Verb = "runas"          //以管理员的身份运行
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot open log directory: {ex.Message}", "Error");
            }

        }

        #endregion

    }


    public interface IProProcessService
    { 
        Task GetWorkingOrderInfo();
    }
    public class ProProcessService : IProProcessService
    {
        public Task GetWorkingOrderInfo()
        {
            return Task.CompletedTask;
        }
    }

    public class WorkingProcess
    {
        public void Init()
        {

        }
    }
}