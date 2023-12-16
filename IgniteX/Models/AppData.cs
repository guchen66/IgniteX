using IgniteX.Shared.Models;
using IgniteX.Shared.Models.OperatePlc;
using Prism.Modularity;
using Prism.Mvvm;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Models
{
    public class AppData : BindableBase
    {
        public static readonly AppData Instance = new Lazy<AppData>(() => new AppData()).Value;

        private string systemName = "电点火管控制1.0";
        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; RaisePropertyChanged(); }
        }

      /*  private User user = new();
        public User CurrentUser
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }
*/
        public AppData()
        {
            Init();
        }
        /*  public SimpleClient<User> sdb = new();

          public List<User> GetTreeListProducts()
          {
              return sdb.GetList().ToList();
          }
  */
        private ObservableCollection<MenuItem> _datas;
        public ObservableCollection<MenuItem> Datas
        {
            get { return _datas; }
            set { SetProperty(ref _datas, value); }
        }       
        private void Init()
        {           
            //左侧菜单导航名
            Datas = new ObservableCollection<MenuItem>
            {
                new MenuItem {  IconKind="\ue63a", Title = "电点火管",  SubName = new ObservableCollection<NavigationItem>
                {
                    new NavigationItem { SubTitle = "点火管管理"},
                    new NavigationItem { SubTitle = "设备状态"},
                   /* new NavigationItem { SubTitle = "数据统计"},
                    new NavigationItem { SubTitle = "热瞬态"},*/
                    
                }},
                new MenuItem {  IconKind="\ue63e", Title = "PLC设置",  SubName = new ObservableCollection<NavigationItem>
                {
                    new NavigationItem { SubTitle = "Plc列表"},
                   
                }},
                new MenuItem {  IconKind="\ue681", Title = "配方设置",  SubName = new ObservableCollection<NavigationItem>
                {
                    new NavigationItem { SubTitle = "配方列表"},
                    new NavigationItem { SubTitle = "配方参数"},
                }},
                new MenuItem { IconKind="\ue871",  Title = "报警管理",  SubName = new ObservableCollection<NavigationItem>
                {
                    new NavigationItem { SubTitle = "报警记录"},
                    new NavigationItem { SubTitle = "历史报警"}
                }},
                new MenuItem { IconKind="\ue69c",  Title = "日志管理",  SubName = new ObservableCollection<NavigationItem>
                {
                   // new NavigationItem { SubTitle = "在线日志"},
                    new NavigationItem { SubTitle = "本地日志"}
                }},
            };
         
        }

        /// <summary>
        /// 可下发订单名称
        /// </summary>
        public IEnumerable<OrderInfoDto> OrderInfos
        {        
            get
            {
                var infos = new List<OrderInfoDto>
                {
                    new OrderInfoDto() { Id = 1, OrderName = "001" },
                    new OrderInfoDto() { Id = 1, OrderName = "002" },
                    new OrderInfoDto() { Id = 1, OrderName = "003" },
                    new OrderInfoDto() { Id = 1, OrderName = "004" }
                };
                return infos;
            }
        }

       
    }
}
