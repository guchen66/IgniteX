
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
using System.Xml.Schema;

namespace IgniteX.UI.ViewModels
{
    public class DeviceStateViewModel : BindableBase
    {

        DeviceStateService db = new DeviceStateService();
        public SimpleClient<DeviceStatus> sdb = new SimpleClient<DeviceStatus>(DatabaseService.GetClient());

        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        //  IEnumerable<WorkPlace> dataList=null;
        public DeviceStateViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            //仓储查询结果是List，转成ObservableCollection
           // db.QueryList().ForEach(x => GridModelList.Add(x));

           // InitQueryAsync();
            this.Refresh();
          
        }
     //   IgniteType

        #region  方法

        /// <summary>
        /// 查询所有List
        /// </summary>
        private async void InitQueryAsync()
        {
            var igniteList = await db.QueryListAsync();
            igniteList.ForEach(x => GridModelList.Add(x));
        }




        #endregion

       
        private ObservableCollection<DeviceStatus> gridModelList = new ObservableCollection<DeviceStatus>();
        public ObservableCollection<DeviceStatus> GridModelList//和前台要对应
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }


        //查询全部
       /* public ObservableCollection<DeviceStatus> SelectAll()
        {
           *//* List<DeviceStatus> stations = new List<DeviceStatus>();
            if (stations != null)
            {
                GridModelList.Clear();
                stations.ForEach(x => GridModelList.Add(x));
            }
            return GridModelList;*//*
        }*/

        //TextBox初始为Empty
        private string search = string.Empty;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        void ExecuteQueryCmd()
        {
            var dataList = db.QueryList().ToList().Where(it => it.Id.ToString().Contains(Search)
            );
            GridModelList = new ObservableCollection<DeviceStatus>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
            }
        }



        //新增
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCmd));

        void ExecuteAddCmd()
        {
            DialogParameters paramters = new DialogParameters();
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("AddWorkStationDialog", paramters, r =>
            {
                /*if (r.Result == ButtonResult.Yes)
                {
                    Refresh();
                }*/
            });
        }


        //修改
        private DelegateCommand<int?> _updateCommand;
        public DelegateCommand<int?> UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand<int?>(ExecuteUpdateCmd));

        private void ExecuteUpdateCmd(int? id)
        {
           /* var dataList = db.GetAllWorkPlaces().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));


            _dialogService.ShowDialog("UpdateWorkPlaceDialog", paramters, r =>
            {

            });*/
        }
       
        /// <summary>
        /// 加一步判断，先打开下载弹窗
        /// </summary>
        private DelegateCommand _downloadCommand;
        public DelegateCommand DownLoadCommand =>
            _downloadCommand ?? (_downloadCommand = new DelegateCommand(ExecuteDownLoadCommand));

        private void ExecuteDownLoadCommand()
        {
            var className=System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;   //获取类名UserViewModel

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

        //删除
        private DelegateCommand<int?> _deleteCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand<int?>(ExecuteDeleCommand));
            set => _deleteCommand = value;
        }



        private async void ExecuteDeleCommand(int? ids)
        {
           /* var model = db.GetAllWorkPlaces().Where(it => it.Id == ids);

            if (model != null)
            {

            }*/


        }

        //打开刷新界面
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCmd));

        private void ExecuteRefreshCmd()
        {
            _dialogService.ShowDialog("RefreshDialog");

        }



        //用来刷新界面
        public void Refresh()
        {
           /*  var dataList = db.QueryList().Where(it => it.OrderNumber == Search);
            GridModelList = new ObservableCollection<DeviceStatus>();
            if (dataList != null)
            {
                db.QueryList().ForEach(x => GridModelList.Add(x));
            }*/
        }


        private async void DoRefresh()
        {
            Search = string.Empty;
            this.Refresh();
            await Task.Delay(3000); // Wait for 3 seconds

        }

    }
}
