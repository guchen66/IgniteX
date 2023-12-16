using IgniteX.UI.Models;
using IgniteX.UI.Views.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar;
using SqlSugarDB.Models;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IgniteX.UI.ViewModels
{
    public class RepiceListViewModel : BindableBase
    {

        public SimpleClient<DataResult> sdb = new SimpleClient<DataResult>(DatabaseService.GetClient());
        DataResultService db = new DataResultService();
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;  //事件管理器发布订阅消息
        List<DataResult> dataList = new List<DataResult>();

        private DispatcherTimer _timer;
        private bool _isDelaying;
        public RepiceListViewModel(IDialogService dialogService,
           IEventAggregator eventAggregator)
        {

            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            // sdb.GetList().ForEach(x => LimitValue.Add(x));
            LimitValue = new ObservableCollection<LimitData>()
            {
                new LimitData(){ Electronic=1.1,Measured=1.2,Upper=2.2,Lower=0.4},
                new LimitData(){ Electronic=1.1,Measured=1.2,Upper=2.2,Lower=0.4},
                new LimitData(){ Electronic=1.1,Measured=1.2,Upper=2.2,Lower=0.4}
            };
        }

        private ObservableCollection<LimitData> _limitValue;
        public ObservableCollection<LimitData> LimitValue//和前台要对应
        {
            get => _limitValue;
            set => SetProperty(ref _limitValue, value); 
        }

        private string _searchContent;

        public string SearchContent
        {
            get { return _searchContent; }
            set { SetProperty<string>(ref _searchContent, value); }
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand = new DelegateCommand(ExecuteConfirm));

        private void ExecuteConfirm()
        {

        }

        //刷新
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCmd));

        private void ExecuteRefreshCmd()
        {
            DoRefresh();

        }

        /// <summary>
        /// 加一步判断，先打开下载弹窗
        /// </summary>
        private DelegateCommand _downloadCommand;
        public DelegateCommand DownLoadCommand =>
            _downloadCommand ?? (_downloadCommand = new DelegateCommand(ExecuteDownLoadCommand));

        private void ExecuteDownLoadCommand()
        {
            var className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;   //获取类名UserViewModel

            string prefixName = className.Substring(0, className.IndexOf("ViewModel"));          //截取

            DialogParameters paramters = new DialogParameters();

            paramters.Add("prefixName", prefixName);
            _dialogService.ShowDialog("ExcelDownDialog", paramters, callback =>
            {
                //判断结果，并进行点击“确定”的后续操作
                if (callback.Result == ButtonResult.OK)
                {
                    ShowFinishWindow();
                }
                else if (callback.Result == ButtonResult.No)
                {
                    ShowErrorWindow();
                }
            });
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        private async void ShowFinishWindow()
        {
            DownFinishDialog popup = new DownFinishDialog();
            // 设置窗口的样式
            popup.WindowStyle = WindowStyle.None;

            // 设置窗口的大小和内容自适应
            popup.SizeToContent = SizeToContent.WidthAndHeight;
            // 设置不显示在任务栏
            popup.ShowInTaskbar = false;

            // 显示弹窗
            popup.Show();
            // 将窗口定位到右下角
            popup.WindowStartupLocation = WindowStartupLocation.Manual;
            popup.Left = SystemParameters.WorkArea.Width - popup.ActualWidth;
            popup.Top = SystemParameters.WorkArea.Height - popup.ActualHeight;
            await Task.Delay(5000);
            // 关闭弹窗
            popup.Close();
        }

        /// <summary>
        /// 下载失败
        /// </summary>
        private async void ShowErrorWindow()
        {
            DownErrorDialog popup = new DownErrorDialog();
            // 设置窗口的样式
            popup.WindowStyle = WindowStyle.None;

            // 设置窗口的大小和内容自适应
            popup.SizeToContent = SizeToContent.WidthAndHeight;
            // 设置不显示在任务栏
            popup.ShowInTaskbar = false;

            // 显示弹窗
            popup.Show();
            // 将窗口定位到右下角
            popup.WindowStartupLocation = WindowStartupLocation.Manual;
            popup.Left = SystemParameters.WorkArea.Width - popup.ActualWidth;
            popup.Top = SystemParameters.WorkArea.Height - popup.ActualHeight;
            await Task.Delay(5000);
            // 关闭弹窗
            popup.Close();
        }

        //当前页码的属性
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
                UpdateData();
            }
        }

        //总页数的属性
        private int _totalPages = 1;
        public int TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }

        //更新数据的方法
        private void UpdateData()
        {
            int pageSize = 15; //每页展示的数据数量
            int startIndex = (CurrentPage - 1) * pageSize;
            int endIndex = CurrentPage * pageSize;

            //获取当前页的数据
            LimitValue.Clear();
            for (int i = startIndex; i < endIndex && i < dataList.Count; i++)
            {
               // LimitValue.Add(dataList[i]);
            }

            //计算总页数
            TotalPages = (int)Math.Ceiling((double)dataList.Count / pageSize);
        }

      
        //用来刷新界面
        public void Refresh()
        {
            //var dataList = db.Queryable.ToList(it => it.Name == Search);
            var dataList = sdb.GetList().Where(it => it.ElectronicName == SearchContent);
            LimitValue.Clear();// = new ObservableCollection<User>();
            if (dataList != null)
            {
               // sdb.GetList().ForEach(x => LimitValue.Add(x));
            }
        }


        private async void DoRefresh()
        {
        
           
            SearchContent = string.Empty;

            this.Refresh();
            await Task.Delay(3000); // Wait for 3 seconds
          
        }

        private void OnDialogClosed(string result)
        {
            // _eventAggregator.GetEvent<DialogColsedEvent>().Publish(result);
        }

    }
}
