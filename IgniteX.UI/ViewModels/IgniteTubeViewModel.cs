using IgniteX.UI.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ReactiveUI;
using SqlSugar;
using SqlSugarDB.Models;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace IgniteX.UI.ViewModels
{
    public class IgniteTubeViewModel : BindableBase
    {
       
        DeviceStateService db = new DeviceStateService();
        public SimpleClient<DeviceStatus> sdb = new SimpleClient<DeviceStatus>(DatabaseService.GetClient());
      
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private string _deviceType="111";

        /// <summary>
        /// 选中的设备名称
        /// </summary>
        private IgniteTubeInfo _deviceItem = new IgniteTubeInfo();

        public IgniteTubeInfo DeviceItem
        {
            get { return _deviceItem; }
            set { SetProperty(ref _deviceItem, value); }
        }


        /// <summary>
        /// 所有设备
        /// </summary>
        private ObservableCollection<LimitData> _igniteList;

        public ObservableCollection<LimitData> IgniteList
        {
            get { return _igniteList; }
            set { SetProperty(ref _igniteList, value); }
        }

        //  IEnumerable<WorkPlace> dataList=null;
        public IgniteTubeViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            //仓储查询结果是List，转成ObservableCollection
              db.QueryList().ForEach(x => GridModelList.Add(x));
             this.Refresh();

            IgniteList = new ObservableCollection<LimitData>()
            {
                new LimitData() {Upper=1,Lower=2 },
                new LimitData() {Upper=1,Lower=2 },
                new LimitData() {Upper=1,Lower=2 },
            };

            IgniteTypes = new ObservableCollection<IgniteType>()
            {
                new IgniteType(){ Name="类型A"},
                new IgniteType(){ Name="类型A"},
                new IgniteType(){ Name="类型A"},
            };
            //  InitQueryAsync();
        }


        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty<string>(ref _selectedItem, value); }
        }

        private ObservableCollection<IgniteType> _igniteType;
        public ObservableCollection<IgniteType> IgniteTypes//和前台要对应
        {
            get { return _igniteType; }
            set { _igniteType = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<DeviceStatus> gridModelList = new ObservableCollection<DeviceStatus>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<DeviceStatus> GridModelList//和前台要对应
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }


        private string _igniteTitle;

        public string IgniteTitle
        {
            get { return _igniteTitle; }
            set { SetProperty<string>(ref _igniteTitle, value); }
        }
        private string _igniteStep;

        public string IgniteStep
        {
            get { return _igniteStep; }
            set { SetProperty<string>(ref _igniteStep, value); }
        }


        #region  方法

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

        /// <summary>
        /// 查询所有List
        /// </summary>
        private async void InitQueryAsync()
        {
            var igniteList = await db.QueryListAsync();
            igniteList.ForEach(x => GridModelList.Add(x));
        }
        #endregion

        //查询全部
        public ObservableCollection<DeviceStatus> SelectAll()
        {
            List<DeviceStatus> stations = new List<DeviceStatus>();
            if (stations != null)
            {
                GridModelList.Clear();
                stations.ForEach(x => GridModelList.Add(x));
            }
            return GridModelList;
        }

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

           /* var dataList = db.GetAllDevice().ToList().Where(it => it.Id.ToString().Contains(Search)
            || it.OrderNumber.Contains(Search)
            );
            GridModelList = new ObservableCollection<DeviceStatus>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));

            }*/
        }



        //新增
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSave));

        private void ExecuteSave()
        {
           
        }


        //修改
        private DelegateCommand<int?> _updateCommand;
        public DelegateCommand<int?> UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand<int?>(ExecuteUpdateCmd));

        private void ExecuteUpdateCmd(int? id)
        {
            /*var dataList = db.GetAllDevice().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));


            _dialogService.ShowDialog("UpdateWorkPlaceDialog", paramters, r =>
            {

            });*/
        }

        //下载
        private DelegateCommand<string> _downloadCommand;
        public DelegateCommand<string> DownLoadCommand =>
            _downloadCommand ?? (_downloadCommand = new DelegateCommand<string>(ExecuteDownLoadCommand));

        private void ExecuteDownLoadCommand(string search)
        {
            var className = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;   //获取类名UserViewModel

            string prefixName = className.Substring(0, className.IndexOf("ViewModel"));          //截取

            DialogParameters paramters = new DialogParameters();

            paramters.Add("prefixName", prefixName);
            _dialogService.ShowDialog("ExcelDownDialog", paramters, arg =>
            {

            });

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
           /* var model = db.GetAllDevice().Where(it => it.Id == ids);

            if (model != null)
            {
                
            }
*/

        }

        //刷新
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCmd));

        private void ExecuteRefreshCmd()
        {
            DoRefresh();


        }



        //用来刷新界面
        public void Refresh()
        {
           // var dataList = db.GetAllDevice().Where(it => it.OrderNumber == Search);

          /*  GridModelList = new ObservableCollection<WorkPlace>();
            if (dataList != null)
            {
                db.GetAllWorkPlaces().ForEach(x => GridModelList.Add(x));
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
